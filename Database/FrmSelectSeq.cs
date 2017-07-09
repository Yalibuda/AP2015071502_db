using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Collections.ObjectModel;


namespace LCY_Database
{
    public partial class FrmSelectSeq : Form
    {
        public FrmSelectSeq(string[] selectitems)
        {
            InitializeComponent();
            _selectedItems = selectitems;
        }

        private string[] _allItems;
        private string[] _selectedItems;        

        /// <summary>
        /// 依據UI條件(上傳 or 未上傳)更新可使用的項目
        /// </summary>
        private void UpdateAllItems()
        {
            bool showUploadedSeq = ckShowUploaded.Checked;
            string connString = LCY_DBTools.DBTool.GetConnString();
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter())
                    {
                        da.SelectCommand = new NpgsqlCommand();
                        da.SelectCommand.Connection = conn;
                        string cmndtext = "select exp_sum_index from exp_sum";
                        if (!showUploadedSeq) cmndtext = cmndtext + " where lot_id is null";
                        cmndtext = cmndtext + " order by exp_sum_index";
                        da.SelectCommand.CommandText = cmndtext;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        _allItems = dt.Rows.Cast<DataRow>().Select(x => x["exp_sum_index"].ToString()).ToArray();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        /// <summary>
        /// 更新 lvAvailable 顯示內容，同時更新:
        /// 
        /// </summary>
        private void Refresh_lvAvailable()
        {
            UpdateAllItems();
            if (_allItems == null || _allItems.Length == 0) return;

            string[] avaliable;
            //
            // 濾出可用的項目資訊
            // 先把已選擇的項目過濾
            //
            if (lvSelected.Items != null && lvSelected.Items.Count > 0)
            {
                string[] selected = lvSelected.Items.Cast<ListViewItem>().Select(x => x.Text).ToArray();
                avaliable = _allItems.Where(x => !selected.Contains(x)).ToArray();
            }
            else
            {
                avaliable = _allItems;
            }

            //
            // 過濾文字篩選項
            //
            string filter = textFilter.Text;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(filter);
                avaliable = avaliable.Where(r => regex.IsMatch(r) == true).ToArray();
            }

            // 轉換為 ListViewItem
            ListViewItem[] lvItems = avaliable.Select(x => new ListViewItem(x)).ToArray();
            lvAvailable.BeginUpdate();
            lvAvailable.Items.Clear();
            if (lvItems != null && lvItems.Length > 0)// 更新 lvAvailable
            {
                lvAvailable.Items.AddRange(lvItems);
            }
            lvAvailable.EndUpdate();

        }
        
        /// <summary>
        /// 讀取已選取的項目，與 all items 比較，將已選取的項目移除並放到 lvSelected 中.
        /// 
        /// </summary>
        /// <param name="items"></param>
        private void LoadItems(string[] items)
        {            
            //
            // 讓符合資料庫的項目讀入(雖然目前不提供使用者編輯來源)
            //
            List<string> validSelected = new List<string>();
            List<string> invalidSelected = new List<string>();
            if (items != null && items.Length > 0)
            {
                foreach (string item in items)
                {
                    if (_allItems.Contains(item))
                    {
                        validSelected.Add(item);//合法的項目
                    }
                    else
                    {
                        invalidSelected.Add(item);//非規範內項目
                    }
                }
                foreach (string item in validSelected)
                {
                    lvSelected.Items.Add(new ListViewItem(item));
                }
                lvSelected.Sorting = SortOrder.Ascending;
                lvSelected.Sort();
            }
        }

        /// <summary>
        /// Move items from lv1 to lv2
        /// </summary>
        /// <param name="lv1"></param>
        /// <param name="lv2"></param>
        private void lvSelectedItemsMoveFrom1To2(ListView lv1, ListView lv2)
        {
            //Move items to lv2
            lv2.BeginUpdate();
            foreach (ListViewItem item in lv1.SelectedItems)
            {
                lv2.Items.Add((ListViewItem)item.Clone());

            }
            lv2.EndUpdate();

            //Remove items from lv1
            lv1.BeginUpdate();
            for (int i = lv1.Items.Count; i-- > 0; )
            {
                ListViewItem lv = lv1.Items[i];
                if (lv.Selected) lv1.Items.RemoveAt(i);
            }
            lv1.EndUpdate();

        }

        /// <summary>
        /// 取得已選擇項
        /// </summary>
        /// <returns></returns>
        public string[] GetSelectedItems()
        {
            return _selectedItems;
        }

        #region 控制項事件
        private void FrmSelectSeq_Load(object sender, EventArgs e)
        {
            textFilter.Text = "";
            //seleted item 可能為已上傳 item, 所以一開始全部載入，loadItem 才能提供正確結果
            ckShowUploaded.Checked = true;
            UpdateAllItems();
            LoadItems(_selectedItems);

            ckShowUploaded.Checked = false; //關閉 show uploaded items 後重新載入
            UpdateAllItems();
            Refresh_lvAvailable();
        }
        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            Refresh_lvAvailable();
        }
        private void ckShowUploaded_CheckedChanged(object sender, EventArgs e)
        {
            Refresh_lvAvailable();
        }
        private void btSelRight_Click(object sender, EventArgs e)
        {
            lvSelectedItemsMoveFrom1To2(lvAvailable, lvSelected);
            lvSelected.Sorting = SortOrder.Ascending;
            lvSelected.Sort();
        }
        private void btSelLeft_Click(object sender, EventArgs e)
        {
            lvSelectedItemsMoveFrom1To2(lvSelected, lvAvailable);
            lvAvailable.Sorting = SortOrder.Ascending;
            lvAvailable.Sort();
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            _selectedItems = lvSelected.Items.Cast<ListViewItem>().Select(x => x.Text).ToArray();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        } 
        #endregion

        



    }
}
