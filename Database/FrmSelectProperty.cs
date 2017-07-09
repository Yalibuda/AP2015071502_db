using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCY_Database
{
    public partial class FrmSelectProperty : Form
    {
        public IItem[] _Property { set; get; }
        public FrmSelectProperty(IItem[] property)
        {
            _Property = property;
            InitializeComponent();
            btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private DataTable _dt = null;
        private string _connString = LCY_DBTools.DBTool.GetConnString();
        private void LoadItems(IItem[] items)
        {
            if (items == null || items.Length == 0) return;
            DataGridViewRow row;
            DataGridViewCell cell;
            DataGridViewComboBoxCell cbCell;
            foreach (IItem item in items)
            {
                row = new DataGridViewRow();
                cell = new DataGridViewTextBoxCell();
                cell.Value = item.Name;
                row.Cells.Add(cell);
                cell = new DataGridViewTextBoxCell();
                cell.Value = item.ProdType;
                row.Cells.Add(cell);
                cbCell = new DataGridViewComboBoxCell();
                cbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                cbCell.FlatStyle = FlatStyle.Popup;
                cbCell.ValueMember = "item_id";
                cbCell.DisplayMember = "unit";
                DataTable unitsource = _dt.Select("item_name='" + item.Name + "'").CopyToDataTable().DefaultView.ToTable(true, "item_id", "unit", "item_flag");
                cbCell.DataSource = unitsource;
                cbCell.Value = unitsource.Select("item_id='" + item.ID + "'").FirstOrDefault()["item_id"];
                row.Cells.AddRange(cbCell);
                dataGridView1.Rows.Add(row);
                //row = new DataGridViewRow();
                //cell = new DataGridViewTextBoxCell();
                //cell.Value = item.Name;
                //cbCell = new DataGridViewComboBoxCell();
                //cbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                //cbCell.FlatStyle = FlatStyle.Popup;
                //cbCell.Items.AddRange(((List<string>)item.Tag).ToArray());
                //cbCell.Value = cbCell.Items[cbCell.Items.IndexOf(item.Unit)];
                //row.Cells.AddRange(cell, cbCell);
                //dataGridView1.Rows.Add(row);

            }
        }

        private void btSel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvSelect.SelectedItems)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
                txtCell.Value = item.Text;
                row.Cells.Add(txtCell);
                txtCell = new DataGridViewTextBoxCell();
                txtCell.Value = item.Group.Name;
                row.Cells.Add(txtCell);
                DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
                cbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                cbCell.FlatStyle = FlatStyle.Popup;
                cbCell.ValueMember = "item_id";
                cbCell.DisplayMember = "unit";
                DataTable unitsource = _dt.Select("item_name='" + item.Text + "'").CopyToDataTable().DefaultView.ToTable(true, "item_id", "unit", "item_flag");
                cbCell.DataSource = unitsource;
                cbCell.Value = unitsource.Rows[0]["item_id"];                
                row.Cells.Add(cbCell);
                cbCell.ReadOnly = true;
                dataGridView1.Rows.Add(row);
                //    DataGridViewRow row = new DataGridViewRow();

                //    DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
                //    txtCell.Value = item.Text;
                //    row.Cells.Add(txtCell);
                //    txtCell = new DataGridViewTextBoxCell();
                //    txtCell.Value = item.Group.Name;
                //    row.Cells.Add(txtCell);
                //    DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
                //    cbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                //    cbCell.FlatStyle = FlatStyle.Popup;
                //    cbCell.Items.AddRange(item.SubItems[1].Text.Split(';'));
                //    cbCell.Value = cbCell.Items[0];
                //    row.Cells.Add(cbCell);
                //    cbCell.ReadOnly = true;
                //    dataGridView1.Rows.Add(row);
            }
            lvSelect.SelectedItems.Clear();
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show(this, "請至少選擇一個物性", "選擇物性", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<IItem> properties = new List<IItem>();
            Item item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                item = new Item();
                item.Name = row.Cells[0].Value.ToString();
                item.ProdType = row.Cells[1].Value.ToString();
                item.ID = row.Cells[2].Value.ToString();
                item.Unit = ((DataGridViewComboBoxCell)row.Cells[2]).FormattedValue.ToString();                
                item.Flag = ((DataTable)((DataGridViewComboBoxCell)row.Cells[2]).DataSource).Select("item_id=" + item.ID).FirstOrDefault()["item_flag"].ToString();
                properties.Add(item);
                //item = new Item();
                //item.Name = row.Cells[0].Value.ToString();
                //item.ProdType = row.Cells[1].Value.ToString();
                //item.Unit = row.Cells[2].Value.ToString();
                //List<string> unitInfo = new List<string>();
                //foreach (var cbitem in ((DataGridViewComboBoxCell)row.Cells[2]).Items)
                //    unitInfo.Add(cbitem.ToString());
                //item.Tag = unitInfo;

                //properties.Add(item);
            }
            _Property = properties.ToArray();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }
        private void lvSelect_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                Rectangle bound = e.Bounds;
                RectangleF rectF = new RectangleF((float)bound.X, (float)bound.Y, (float)bound.Width - 1.0f, (float)bound.Height - 1.0f);
                rectF.Inflate(-1, -1);
                LCY_Database.Tools.DrawingTool.DrawRoundedRectangle(e.Graphics, rectF, 3, new Pen(Color.DodgerBlue, 1));
            }
            //if (((ListView)sender).View != View.Details)
            //{
            //    e.DrawText();
            //}
            e.DrawText();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView datagridview = (DataGridView)sender;
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                datagridview.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void FrmSelectProperty_Load(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select distinct item_id, item_name, unit, item_flag, type from item where item_flag like'P%' order by item_flag, item_name";
                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    ListViewItem lvItem = new ListViewItem();
                    DataTable itemname = dt.DefaultView.ToTable(true, "item_name");

                    foreach (DataRow item in itemname.Rows)
                    {
                        var data = dt.AsEnumerable().Where(x => x.Field<string>("item_name") == item["item_name"].ToString()).
                            Select(x =>
                                new { UNIT = x.Field<string>("unit"), TYPE = x.Field<string>("type") }).ToArray();
                        lvItem = new ListViewItem();
                        lvItem.Text = item["item_name"].ToString();
                        lvItem.SubItems.Add(string.Join(";", data.Select(x => x.UNIT).ToArray()));
                        lvItem.SubItems.Add(data.Select(x => x.TYPE).FirstOrDefault());
                        lvSelect.Items.Add(lvItem);
                    }

                    //建立 group 資訊
                    System.Collections.Hashtable ht = new System.Collections.Hashtable();
                    foreach (ListViewItem item in lvSelect.Items)
                    {
                        string gpname = item.SubItems[2].Text;
                        if (!ht.Contains(gpname)) ht.Add(gpname, new ListViewGroup(gpname, HorizontalAlignment.Left) { Name = gpname });
                    }
                    ListViewGroup[] groupsArray = new ListViewGroup[ht.Count];
                    ht.Values.CopyTo(groupsArray, 0);
                    lvSelect.Groups.AddRange(groupsArray);

                    foreach (ListViewItem item in lvSelect.Items)
                    {
                        string gpname = item.SubItems[2].Text;
                        item.Group = (ListViewGroup)ht[gpname];
                    }

                }
                _dt = dt;

            }
            LoadItems(_Property);
        }
    }
}
