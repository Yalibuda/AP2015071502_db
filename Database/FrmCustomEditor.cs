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

namespace LCY_Database
{
    public partial class FrmCustomEditor : Form
    {
        public FrmCustomEditor(string sqlString, object items = null)
        {
            this._sqlString = sqlString;
            try
            {
                this.Items = (IItem[])items;
            }
            catch (Exception)
            {
                this.Items = null;
            }

            InitializeComponent();
        }

        private string _sqlString;
        private string _connString = LCY_DBTools.DBTool.GetConnString();
        public IItem[] Items { private set; get; }

        private void FrmCustomEditor_Load(object sender, EventArgs e)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = _sqlString;
                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;

                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Name = "item_value";
                col.HeaderText = "Value";
                dataGridView1.Columns.Insert(2, col);
                dataGridView1.Columns["item_id"].Visible = false;
                dataGridView1.Columns["tool_id"].Visible = false;
                dataGridView1.Columns["item_flag"].Visible = false;
                dataGridView1.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (Items != null)
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    dataGridView1.Rows[i].Cells["item_value"].Value = Items[i].Value;
                }
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            List<IItem> items = new List<IItem>();
            IItem item;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                item = new Item();
                item.ID = row.Cells["item_id"].Value.ToString();
                item.Name = row.Cells["name"].Value.ToString();
                item.Unit = row.Cells["unit"].Value.ToString();
                item.ProdType = "";
                item.Value = row.Cells["item_value"].Value == null ? "" : row.Cells["item_value"].Value.ToString();
                items.Add(item);
            }
            Items = items.ToArray();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
