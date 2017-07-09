using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LCY_Database.Tools;
using Npgsql;

namespace LCY_Database
{
    public partial class FrmSelectLot : Form
    {
        public FrmSelectLot(string[] lots)
        {
            _selectlots = lots;
            InitializeComponent();
        }


        private string[] _lots;
        private string[] _selectlots;//current selected items
        private string _connString = LCY_DBTools.DBTool.GetConnString();


        private List<ListViewItem> _allLvItems = new List<ListViewItem>();//all available lots 的 lvItem，用於文字搜尋
        /// <summary>
        /// 讀取輸入的 lots 陣列(已選取)，與 full lots 比較，將已選取的 lot 放到 lvSelectedLots 中.
        /// 
        /// </summary>
        /// <param name="lots"></param>
        private void LoadItems(string[] lots)
        {
            if (lots != null && lots.Length > 0)
            {
                List<string> tmpLots = _lots.ToList(); //把 available lots array 轉換成 list 方便編輯
                for (int i = tmpLots.Count; i-- > 0; ) //將已選擇項自 full item (lvSel) 和 _allLvItems (查詢用) 中移除
                {
                    if (lots.Contains(tmpLots[i]))
                    {                       
                        lvSelect.Items.RemoveAt(i);
                        int id = _allLvItems.Select((r, j) => object.Equals(r.Text, tmpLots[i]) ? j : -1).Where(r => r != -1).FirstOrDefault();
                        _allLvItems.RemoveAt(id);
                        tmpLots.RemoveAt(i);
                        
                    }
                }
                _lots = tmpLots.ToArray();//把編輯後的 available lots 回填給 lots array
                ListViewItem lv; //暫存用的 lvitem                
                foreach (string lot in lots)//把選擇項填入 lvSelectedLot 中
                {
                    lv = new ListViewItem(lot);
                    lvSelectedLots.Items.Add(lv);
                }
                lvSelectedLots.Sorting = SortOrder.Ascending;
                lvSelectedLots.Sort();
            }
        }
        /// <summary>
        /// 取得已選取的 lots 資訊
        /// </summary>
        /// <returns>lot 的字串陣列</returns>
        public string[] GetSelectedLots()
        {
            if (_selectlots != null)
            {
                return _selectlots;
            }
            else
            {
                return null;
            }
        }

        //
        // textFilter
        //
        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            lvSelect.Items.Clear();
            TextBox tbox = (TextBox)sender;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(tbox.Text);

            ListViewItem[] lvItems = _allLvItems
                .Where(r => regex.IsMatch(r.Text) == true).ToArray();

            if (lvItems != null && lvItems.Length > 0)
            {
                lvSelect.Items.AddRange(lvItems);
            }
        }
        //
        // btSel
        //
        private void btSel_Click(object sender, EventArgs e)
        {
            List<string> tmpLots = _lots.ToList();
            foreach (ListViewItem item in lvSelect.SelectedItems)
            {
                lvSelectedLots.Items.Add((ListViewItem)item.Clone());
                lvSelect.Items.RemoveAt(item.Index);
                int id = _allLvItems.Select((r, i) => object.Equals(r.Text, item.Text) ? i : -1).Where(r => r != -1).FirstOrDefault();
                _allLvItems.RemoveAt(id);
            }
            lvSelectedLots.Sorting = SortOrder.Ascending;
            lvSelectedLots.Sort();

        }
        //
        // btDel
        //
        private void btDel_Click(object sender, EventArgs e)
        {
            List<string> tmpLots = _lots.ToList();
            foreach (ListViewItem item in lvSelectedLots.SelectedItems)
            {
                lvSelect.Items.Add((ListViewItem)item.Clone());
                _allLvItems.Add((ListViewItem)item.Clone());
                lvSelectedLots.Items.RemoveAt(item.Index);
            }
            _allLvItems.Sort(new ListViewItemComparer());
            lvSelect.Sorting = SortOrder.Ascending;
            lvSelect.Sort();
        }
        //
        // btOK
        //
        private void btOK_Click(object sender, EventArgs e)
        {
            List<string> selectedlots = new List<string>();
            foreach (ListViewItem item in lvSelectedLots.Items)
            {
                selectedlots.Add(item.Text);
            }
            _selectlots = selectedlots.ToArray();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        //
        // btCancel
        //
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        //
        // lvSelect
        //
        private void lvSelect_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                Rectangle bound = e.Bounds;
                RectangleF rectF = new RectangleF((float)bound.X, (float)bound.Y, (float)bound.Width - 1.0f, (float)bound.Height - 1.0f);
                rectF.Inflate(-1, -1);
                LCY_Database.Tools.DrawingTool.DrawRoundedRectangle(e.Graphics, rectF, 3, new Pen(Color.DodgerBlue, 1));
            }
            if (((ListView)sender).View != View.Details)
            {
                e.DrawText();
            }
        }
        //
        // lvSelectedLots
        //
        private void lvSelectedLots_KeyDown(object sender, KeyEventArgs e)
        {
            ListView lv = (ListView)sender;
            //處理特殊組合鍵
            if (e.KeyCode == Keys.A & e.Control)//Ctrl+A 全選清單中項目
            {
                foreach (ListViewItem items in lv.Items)
                    items.Selected = true;
                return;
            }
        }

        private void FrmSelectLot_Load(object sender, EventArgs e)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("select distinct lot_id from exp_sum where lot_id is not null order by lot_id", conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        _lots = dt.Rows.Cast<DataRow>().Select(x => x["lot_id"].ToString()).ToArray();

                        //
                        // Add listviewitem to listview
                        //                        
                        ListViewItem lv;
                        foreach (string item in _lots)
                        {
                            lv = new ListViewItem(item);
                            lvSelect.Items.Add(lv);
                            _allLvItems.Add(lv);
                        }
                        textFilter.Text = "";
                    }
                }
                //讀取已選擇項
                LoadItems(_selectlots);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
