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
    public partial class FrmQuery : Form
    {
        public FrmQuery()
        {
            InitializeComponent();
        }

        public FrmQuery(Mtb.Project proj)
        {
            InitializeComponent();
            _proj = proj;
        }

        private Mtb.Project _proj;
        private List<IItem> oFormulaCollection = new List<IItem>();
        private List<IItem> oPropertyCollection = new List<IItem>();
        private List<string> oProdTypeCollection = new List<string>();
        private List<string> oLotCollection = new List<string>();
        private ITool CompTool = new Tool();
        private ITool InjTool = new Tool();
        private string _connString = LCY_DBTools.DBTool.GetConnString();


        private void btREdit_Click(object sender, EventArgs e)
        {
            using (FrmSelectFormula f = new FrmSelectFormula(oFormulaCollection.ToArray()))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    List<IItem> formulas = f._Formulas.ToList();
                    //整理資料表
                    //移除不在新項目的欄位
                    for (int i = oFormulaCollection.Count; i-- > 0; )
                    {
                        if (!ItemFunction.CompareFormulationCollection(formulas, oFormulaCollection[i]))
                        {
                            IItem item = oFormulaCollection[i];
                            string name = item.Name + "[" + item.Unit + "]";
                            int rowid = dgViewFormula.Rows.IndexOf(dgViewFormula.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[0].Value.ToString() == name));
                            dgViewFormula.Rows.RemoveAt(rowid);
                            oFormulaCollection.RemoveAt(i);
                        }

                    }

                    //新增欄位於表格中    
                    DataGridViewRow row;
                    for (int i = 0; i < formulas.Count; i++)
                    {
                        if (oFormulaCollection.Count == 0 || !ItemFunction.CompareFormulationCollection(oFormulaCollection, formulas[i]))
                        {
                            IItem item = formulas[i];
                            string name = item.Name + "[" + item.Unit + "]";
                            row = new DataGridViewRow();
                            row.Cells.Add(new DataGridViewTextBoxCell());
                            row.Cells[0].Value = item.ID;
                            row.Cells.Add(new DataGridViewTextBoxCell());
                            row.Cells[1].Value = name;
                            dgViewFormula.Rows.Insert(i, row);
                        }
                    }
                    oFormulaCollection = formulas;
                }
            }
        }
        private void btPEdit_Click(object sender, EventArgs e)
        {
            using (FrmSelectProperty f = new FrmSelectProperty(oPropertyCollection.ToArray()))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    List<IItem> properties = f._Property.ToList();
                    //整理資料表
                    //移除不在新項目的欄位
                    for (int i = oPropertyCollection.Count; i-- > 0; )
                    {
                        if (!ItemFunction.ComparePropertyCollection(properties, oPropertyCollection[i]))
                        {
                            IItem item = oPropertyCollection[i];
                            string name = item.Name + "[" + item.Unit + "]";
                            int rowid = dgViewFormula.Rows.IndexOf(dgViewFormula.Rows.Cast<DataGridViewRow>().FirstOrDefault(r => r.Cells[0].Value.ToString() == name));
                            dgViewProperty.Rows.RemoveAt(rowid);
                            oPropertyCollection.RemoveAt(i);
                        }
                    }

                    //新增欄位於表格中    
                    DataGridViewRow row;
                    for (int i = 0; i < properties.Count; i++)
                    {
                        if (oPropertyCollection.Count == 0 || !ItemFunction.ComparePropertyCollection(oPropertyCollection, properties[i]))
                        {
                            IItem item = properties[i];
                            string name = item.Name + "[" + item.Unit + "]";
                            row = new DataGridViewRow();
                            row.Cells.Add(new DataGridViewTextBoxCell());
                            row.Cells[0].Value = item.ID;
                            row.Cells.Add(new DataGridViewTextBoxCell());
                            row.Cells[1].Value = name;
                            row.Cells.Add(new DataGridViewTextBoxCell());
                            row.Cells[2].Value = item.ProdType;
                            dgViewProperty.Rows.Insert(i, row);
                        }
                    }
                    oPropertyCollection = properties;
                }
            }
        }
        private void btProdType_Click(object sender, EventArgs e)
        {
            using (FrmSelectProd f = new FrmSelectProd(oProdTypeCollection.ToArray()))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    oProdTypeCollection = f._prods.ToList();
                    textProdType.Text = string.Join(", ", f._prods);
                }
            }
        }
        private void btLot_Click(object sender, EventArgs e)
        {
            using (FrmSelectLot f = new FrmSelectLot(oLotCollection.ToArray()))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    oLotCollection = f.GetSelectedLots().ToList();
                    this.textLot.Text = string.Join(",", oLotCollection.ToArray());
                }
            }
        }
        private void FrmQuery_Load(object sender, EventArgs e)
        {
            try
            {
                //Get the user info
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select emp_id, emp_ename from employee ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow dr = dt.NewRow();
                    dr["emp_id"] = null;
                    dr["emp_ename"] = null;
                    dt.Rows.InsertAt(dr, 0);
                    cbUser.ValueMember = "emp_id";
                    cbUser.DisplayMember = "emp_ename";
                    cbUser.DataSource = dt;
                }
                //Get the customer info
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select customer_id, customer_cname from customer order by customer_ename, customer_cname", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow dr = dt.NewRow();
                    dr["customer_id"] = null;
                    dr["customer_cname"] = null;
                    dt.Rows.InsertAt(dr, 0);
                    cbCustomer.ValueMember = "customer_id";
                    cbCustomer.DisplayMember = "customer_cname";
                    cbCustomer.DataSource = dt;
                }
                //Get purpose
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select purpose from exp_sum ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                }
                //Get Compounding tool info
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select tool_id, tool_name from tool_sum where tool_type like 'C%' ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DataRow dr = dt.NewRow();
                    dr["tool_id"] = null;
                    dr["tool_name"] = null;
                    dt.Rows.InsertAt(dr, 0);
                    cbCTool.ValueMember = "tool_id";
                    cbCTool.DisplayMember = "tool_name";
                    cbCTool.DataSource = dt;
                }

                this.ActiveControl = cbUser;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void dgViewProperty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView datagridview = (DataGridView)sender;
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                datagridview.Rows.RemoveAt(e.RowIndex);
                oPropertyCollection.RemoveAt(e.RowIndex);
            }
        }
        private void dgViewFormula_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView datagridview = (DataGridView)sender;
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                string id = datagridview.Rows[e.RowIndex].Cells[0].Value.ToString();
                datagridview.Rows.RemoveAt(e.RowIndex);
                oFormulaCollection.RemoveAt(e.RowIndex);
            }
        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            
            //
            // 收集資訊
            //
            //User
            string user_id = "";
            if (!string.IsNullOrWhiteSpace(cbUser.Text))
            {
                user_id = "'" + cbUser.SelectedValue.ToString() + "'";
            }

            //Purpose
            string purpose = "";
            if (!string.IsNullOrWhiteSpace(textPurpose.Text))
            {
                purpose = "'%" + textPurpose.Text.ToUpper() + "%'";
            }

            //Prod type
            string prodType = "";
            if (oProdTypeCollection != null && oProdTypeCollection.Count > 0)
            {
                List<string> tmp = new List<string>();
                foreach (string item in oProdTypeCollection)
                {
                    tmp.Add("'" + item + "'");
                }
                prodType = string.Join(",", tmp);
            }

            //C tool
            string c_tool = "";
            if (!string.IsNullOrWhiteSpace(cbCTool.Text))
            {
                c_tool = "'" + cbCTool.SelectedValue.ToString() + "'";
            }

            //Lot
            string lot = "";
            if (oLotCollection != null && oLotCollection.Count > 0)
            {
                List<string> tmp = new List<string>();
                tmp = oLotCollection.Select(x => "'" + x + "'").ToList();
                lot = string.Join(",", tmp);
            }

            //customer
            string customer_id = "";
            if (!string.IsNullOrWhiteSpace(cbCustomer.Text))
            {
                customer_id = "'" + cbCustomer.SelectedValue.ToString() + "'";
            }

            //datetime
            string start_date_time = "'" + dtPickerExpStartDate.Value.ToString("yyyy-MM-dd") + "'";
            string end_date_time = "'" + dtPickerExpEndDate.Value.ToString("yyyy-MM-dd") + "'";

            try
            {
                List<string> conditionList = new List<string>();
                //組出基本條件
                if (user_id != "") conditionList.Add(string.Format("a.emp_id={0}", user_id));
                if (purpose != "") conditionList.Add(string.Format("upper(a.purpose) like {0}", purpose));
                if (prodType != "") conditionList.Add(string.Format("a.type in ({0})", prodType));
                if (c_tool != "") conditionList.Add(string.Format("a.c_tool_id={0}", c_tool));
                if (lot != "") conditionList.Add(string.Format("a.lot_id in ({0})", lot));
                conditionList.Add(string.Format("a.exp_date between {0} and {1}", start_date_time, end_date_time));
                string conditionBasic = string.Join(" and ", conditionList);


                Dictionary<string, string> pivotInfo = new Dictionary<string, string>();
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    //先把大條件下的資料抓出，確認有多少 item 要 pivot
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("", conn))
                    {
                        string commandstring = "select distinct b.item_id, b.item_name,b.item_flag, b.unit, a.type from " +
                            "exp_sum a, exp_detail b " +
                            "where a.exp_sum_index = b.exp_sum_index and a.lot_id is not null and b.item_flag ~ '[PR]+' " +
                             (string.IsNullOrEmpty(conditionBasic) ? "" : " and " + conditionBasic) +
                             " order by b.item_flag desc";
                        da.SelectCommand.CommandText = commandstring;
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        //pivotCols = dt.Rows.Cast<DataRow>().Select(x => x["item_name"].ToString()).ToArray();
                        //pivotItemId = dt.Rows.Cast<DataRow>().Select(x => x["item_id"].ToString()).ToArray();
                        pivotInfo = dt.Rows.Cast<DataRow>().ToDictionary(x => x["item_id"].ToString(),
                            x => x["item_name"].ToString() + "[" + x["unit"].ToString() + "]" +
                            (x["item_flag"].ToString() == "P" ? string.Format("{{{0}}}", x["type"].ToString()) : "")
                            );
                    }
                }

                //建立 pivot 後的欄位組
                if (pivotInfo == null || pivotInfo.Count == 0)
                {
                    MessageBox.Show(null, "無符合條件資料", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                //
                // 處理子條件 (配方、物性指定項或上下限)，同時過濾出主條件中無法查詢之項目，
                // 因為 pivot 後項目會變成欄位，不存在的項目會引起查詢錯誤。
                //
                Dictionary<string, string> invalidItems = new Dictionary<string, string>();
                //取出 Recipe 的條件
                conditionList.Clear();
                foreach (DataGridViewRow item in dgViewFormula.Rows)
                {
                    string id = item.Cells["colRId"].Value.ToString();
                    if (pivotInfo.ContainsKey(id))
                    {
                        if (item.Cells["colRLower"].Value != null && !string.IsNullOrWhiteSpace(item.Cells["colRLower"].Value.ToString()))
                        {
                            conditionList.Add(string.Format("\"{0}\"::numeric>={1}", id, item.Cells["colRLower"].Value.ToString()));
                        }
                        if (item.Cells["colRUpper"].Value != null && !string.IsNullOrWhiteSpace(item.Cells["colRUpper"].Value.ToString()))
                        {
                            conditionList.Add(string.Format("\"{0}\"::numeric<={1}", id, item.Cells["colRUpper"].Value.ToString()));
                        }
                        if ((item.Cells["colRLower"].Value == null || string.IsNullOrWhiteSpace(item.Cells["colRLower"].Value.ToString())) //如果沒有填上下限，表示實驗中一定要包含此項
                            && (item.Cells["colRUpper"].Value == null || string.IsNullOrWhiteSpace(item.Cells["colRUpper"].Value.ToString())))
                        {
                            conditionList.Add(string.Format("\"{0}\" is not null", id));
                        }
                    }
                    else
                    {
                        //因為 UI 不卡重複項目，所以要避免加入相同 id
                        if (!invalidItems.ContainsKey(id))
                            invalidItems.Add(id, item.Cells["colRName"].Value.ToString());
                    }

                }
                //取出 Property 的條件
                foreach (DataGridViewRow item in dgViewProperty.Rows)
                {
                    string id = item.Cells["colPId"].Value.ToString();
                    if (pivotInfo.ContainsKey(id))
                    {
                        if (item.Cells["colPLower"].Value != null && !string.IsNullOrWhiteSpace(item.Cells["colPLower"].Value.ToString()))
                        {
                            conditionList.Add(string.Format("\"{0}\"::numeric>={1}", id, item.Cells["colPLower"].Value.ToString()));
                        }
                        if (item.Cells["colPUpper"].Value != null && !string.IsNullOrWhiteSpace(item.Cells["colPUpper"].Value.ToString()))
                        {
                            conditionList.Add(string.Format("\"{0}\"::numeric<={1}", id, item.Cells["colPUpper"].Value.ToString()));
                        }
                        if ((item.Cells["colPLower"].Value == null || string.IsNullOrWhiteSpace(item.Cells["colPLower"].Value.ToString())) //如果沒有填上下限，表示實驗中一定要包含此項
                            && (item.Cells["colPUpper"].Value == null || string.IsNullOrWhiteSpace(item.Cells["colPUpper"].Value.ToString())))
                        {
                            conditionList.Add(string.Format("\"{0}\" is not null", id));
                        }
                    }
                    else
                    {
                        //因為 UI 不卡重複項目，所以要避免加入相同 id
                        if (!invalidItems.ContainsKey(id))
                            invalidItems.Add(id, item.Cells["colPName"].Value.ToString());
                    }

                }

                //若存在無效的條件，詢問是否繼續查詢?
                if (invalidItems.Count > 0)
                {
                    string msg = string.Join(", ", invalidItems.Select(x => x.Value).ToArray());
                    if (MessageBox.Show(this,
                        string.Format("以下查詢項目:\r\n{0}\r\n未出現在實驗結果或指定的查詢範圍內。\r\n\r\n是否忽略這些條件繼續查詢?", msg), "",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information
                        ) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }

                //建立最後要使用的配方、物性條件
                string conditionAddtional = string.Join(" and ", conditionList);

                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("", conn))
                    {
                        List<string> ctstring = new List<string>();
                        ctstring.Add("exp_sum_index text");
                        foreach (string itemId in pivotInfo.Keys.OrderBy(x=>x))
                        {
                            ctstring.Add(@"""" + itemId + @""" text");
                        }

                        //
                        // crosstab 裡面要用單引號
                        //

                        da.SelectCommand.CommandText = "with t1 as (select * from crosstab($$select a.exp_sum_index::text, b.item_id::text, b.item_value::text from exp_sum a " +
                            "join exp_detail b on a.exp_sum_index = b.exp_sum_index where a.lot_id is not null " +
                            "and b.item_flag~'[PR]+'" +
                            (string.IsNullOrEmpty(conditionBasic) ? "" : " and " + conditionBasic) +
                            " order by 1,2 $$) AS ct(" + string.Join(",", ctstring) + ")) " +
                            "select a.lot_id, b.emp_ename, c.customer_ename, a.c_tool_id, a.i_tool_id, a.type, d.* , a.remark " +
                            "from exp_sum a, employee b, customer c, t1 d " +
                            "where a.exp_sum_index = d.exp_sum_index " +
                            "and a.customer_id = c.customer_id " +
                            "and a.emp_id = b.emp_id" +
                            //add condition for r_item and p_item conditionAddtional
                            (string.IsNullOrEmpty(conditionAddtional) ? "" : " and " + conditionAddtional);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        for (int i = 6; i < dt.Columns.Count - 1; i++) // r_item and p_item start from 7th and the last two columns
                        {
                            DataColumn col = dt.Columns[i];
                            string colName;
                            pivotInfo.TryGetValue(col.ColumnName, out colName);
                            col.ColumnName = colName ?? col.ColumnName;

                        }

                        //
                        // 把資料回填 Minitab
                        //
                        if (_proj != null)
                        {
                            //Mtb.Worksheet ws = _proj.ActiveWorksheet;
                            Mtb.Worksheet ws = _proj.Worksheets.Add(1);
                            string[] colIdString = Tools.MtbTools.CreateVariableStrArray(ws, dt.Columns.Count, MtbVarType.Column);

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                DataColumn col = dt.Columns[i];
                                ws.Columns.Item(colIdString[i]).Name = col.ColumnName;
                                var dataarray = dt.Rows.Cast<DataRow>().Select(x => x[col.ColumnName] == System.DBNull.Value ? "" : x[col.ColumnName]).ToArray();
                                ws.Columns.Item(colIdString[i]).SetData(dataarray);
                            }
                            ws.Columns.Item("exp_sum_index").Delete();
                            ws = null;
                        }
                        

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "查詢時發生錯誤: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                MessageBox.Show(null, "查詢完成 ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }



        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }

                _proj = null;
            }

            base.Dispose(disposing);
        }



        //private void textBox_KeyUp(object sender, KeyEventArgs e)
        //{
        //    TextBox tBox = (TextBox)sender;
        //    var x = tBox.Left + tBox.GetPositionFromCharIndex(tBox.SelectionStart).X;
        //    var y = textBox.Top + textBox.Height;
        //    var width = textBox.Width + 20;
        //    const int height = 40;

        //    listBox.SetBounds(x, y, width, height);
        //    listBox.KeyDown += listBox_SelectedIndexChanged;

        //    string aa = textBox.Text.Substring(textBox.SelectionStart);
        //    List<string> localList = prods.Where(z => z.StartsWith(aa)).ToList();
        //    if (localList.Any() && !string.IsNullOrEmpty(aa))
        //    {
        //        listBox.DataSource = localList;
        //        listBox.Show();
        //        listBox.Focus();

        //    }
        //}

        //private void listBox_SelectedIndexChanged(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyValue == (decimal)Keys.Enter)
        //    {
        //        textBox.SelectedText = ((ListBox)sender).SelectedItem.ToString();
        //        listBox.Hide();
        //    };
        //}





    }
}
