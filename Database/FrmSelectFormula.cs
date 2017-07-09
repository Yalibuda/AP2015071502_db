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
    public partial class FrmSelectFormula : Form
    {
        public FrmSelectFormula(IItem[] formulars)
        {
            _Formulas = formulars;
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
                cbCell = new DataGridViewComboBoxCell();
                cbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cbCell.FlatStyle = FlatStyle.Popup;
                cbCell.ValueMember = "item_id";
                cbCell.DisplayMember = "unit";
                DataTable unitsource = _dt.Select("item_name='" + item.Name + "'").CopyToDataTable().DefaultView.ToTable(true, "item_id", "unit", "item_flag");
                cbCell.DataSource = unitsource;
                cbCell.Value = unitsource.Select("item_id='" + item.ID + "'").FirstOrDefault()["item_id"];
                row.Cells.AddRange(cell, cbCell);
                dataGridView1.Rows.Add(row);
            }
        }

        public IItem[] _Formulas { set; get; }
        private void btSel_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvSelect.SelectedItems)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell txtCell = new DataGridViewTextBoxCell();
                txtCell.Value = item.Text;
                row.Cells.Add(txtCell);
                DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
                cbCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
                cbCell.FlatStyle = FlatStyle.Popup;
                //cbCell.Items.AddRange(item.SubItems[1].Text.Split(';'));                
                cbCell.ValueMember = "item_id";
                cbCell.DisplayMember = "unit";
                DataTable unitsource = _dt.Select("item_name='" + item.Text + "'").CopyToDataTable().DefaultView.ToTable(true, "item_id", "unit", "item_flag");
                cbCell.DataSource = unitsource;

                //cbCell.Value = cbCell.Items[0];
                cbCell.Value = unitsource.Rows[0]["item_id"];

                row.Cells.Add(cbCell);
                dataGridView1.Rows.Add(row);
            }
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.RowCount == 0)
            {
                MessageBox.Show(this, "請至少選擇一個配方", "選擇配方", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<IItem> formulas = new List<IItem>();
            IItem formula;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                formula = new Item();
                formula.Name = row.Cells[0].Value.ToString();
                formula.ID = row.Cells[1].Value.ToString();
                formula.Unit = ((DataGridViewComboBoxCell)row.Cells[1]).FormattedValue.ToString();
                formula.ProdType = "";
                formula.Flag = ((DataTable)((DataGridViewComboBoxCell)row.Cells[1]).DataSource).Select("item_id=" + formula.ID).FirstOrDefault()["item_flag"].ToString();
                formulas.Add(formula);
            }
            _Formulas = formulas.ToArray();
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
            if (((ListView)sender).View != View.Details)
            {
                e.DrawText();
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView datagridview = (DataGridView)sender;
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                datagridview.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void FrmSelectFormula_Load(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select distinct item_id, item_name, unit, item_flag from item where item_flag like'R%' order by item_flag, unit,item_name";
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
                        string[] unit = dt.AsEnumerable().Where(x => x.Field<string>("item_name") == item["item_name"].ToString()).
                            Select(x => x.Field<string>("unit")).ToArray();
                        lvItem = new ListViewItem();
                        lvItem.Text = item["item_name"].ToString();
                        lvItem.SubItems.Add(string.Join(";", unit));
                        lvSelect.Items.Add(lvItem);
                    }
                }
                _dt = dt;
            }
            LoadItems(_Formulas);
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;            
        }
    }
}
