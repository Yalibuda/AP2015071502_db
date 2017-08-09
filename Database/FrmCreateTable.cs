using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using NpgsqlTypes;
using Excel = Microsoft.Office.Interop.Excel;

namespace LCY_Database
{
    public partial class FrmCreateTable : Form
    {
        public FrmCreateTable()
        {
            InitializeComponent();
#if DEBUG
            //塞入基本資訊

#endif
        }

        public FrmCreateTable(UserType userType)
        {
            InitializeComponent();
            RefreshDialogByLogonStatus(userType);
            _userType = userType;
        }

        private void RefreshDialogByLogonStatus(UserType userType)
        {
            switch (userType)
            {
                case UserType.ADMIN:
                    ckCompetitor.Enabled = true;
                    break;
                default:
                    ckCompetitor.Checked = false;
                    ckCompetitor.Enabled = false;
                    break;
            }
        }
        
        private UserType _userType = UserType.USER;
        private List<IItem> oFormulaCollection = new List<IItem>();
        private List<IItem> oPropertyCollection = new List<IItem>();
        private ITool CompTool = new Tool();
        private ITool InjTool = new Tool();
        private Testplan compProcessSetting = new Testplan();
        private Testplan compTempSetting = new Testplan();
        private Testplan injProcessSetting = new Testplan();
        private Testplan injTempSetting = new Testplan();
        private DataTable _dtCTestplan = null;
        private DataTable _dtITestplan = null;
        private FrmProgress progressDialog;
        private static Excel.Application _excel;
        private string _excelSavePath = "";
        private string _connString = LCY_DBTools.DBTool.GetConnString();
        private DataGridView _dgview = null;

        #region 元件事件
        private void btFormulation_Click(object sender, EventArgs e)
        {
            using (FrmSelectFormula f = new FrmSelectFormula(oFormulaCollection.ToArray()))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    List<IItem> formulas = f._Formulas.ToList();
                    ListViewItem item;
                    lvFormula.Items.Clear();//漿顯示在表單上的項目清空
                    foreach (IItem formula in formulas)
                    {
                        item = new ListViewItem();
                        item.Text = formula.Name.ToString();
                        item.ToolTipText = string.Format("{0}({1})", formula.Name, formula.Unit);
                        item.SubItems.Add(formula.Unit.ToString());
                        lvFormula.Items.Add(item);
                    }
                    //整理資料表
                    //移除不在新項目的欄位
                    for (int i = oFormulaCollection.Count; i-- > 0; )
                    {
                        //if (formulas.Select((x, j) =>
                        //    object.Equals(x.Name + x.Unit, oFormulaCollection[i].Name + oFormulaCollection[i].Unit) ? j : -1).Where(x => x != -1).FirstOrDefault() == 0 &&
                        //    formulas[0].Name + formulas[0].Unit != oFormulaCollection[i].Name + oFormulaCollection[i].Unit)
                        if (!CompareFormulaCollection(formulas, oFormulaCollection[i]))
                        {
                            string name = string.Format("col_{0}",
                                oFormulaCollection[i].ID);
                            dataGridView1.Columns.Remove(name);
                            oFormulaCollection.RemoveAt(i);
                        }
                    }

                    //新增欄位於表格中
                    DataGridViewTextBoxColumn tcol;
                    for (int i = 0; i < formulas.Count; i++)
                    {
                        //if (oFormulaCollection.Count == 0 || (oFormulaCollection.Select((x, j) =>
                        //       object.Equals(x.Name + x.Unit, formulas[i].Name + formulas[i].Unit) ? j : -1).Where(x => x != -1).FirstOrDefault() == 0 &&
                        //       oFormulaCollection[0].Name + oFormulaCollection[0].Unit != formulas[i].Name + formulas[i].Unit))
                        if (oFormulaCollection.Count == 0 || !CompareFormulaCollection(oFormulaCollection, formulas[i]))
                        {
                            tcol = new DataGridViewTextBoxColumn();
                            tcol.SortMode = DataGridViewColumnSortMode.NotSortable;

                            tcol.Name = string.Format("col_{0}",
                                formulas[i].ID);
                            tcol.HeaderText = string.Format("{0}[{1}]", formulas[i].Name, formulas[i].Unit);
                            dataGridView1.Columns.Insert(1 + i, tcol);
                        }
                    }
                    oFormulaCollection = formulas;
                }
            }
        }
        private void btProperty_Click(object sender, EventArgs e)
        {
            using (FrmSelectProperty f = new FrmSelectProperty(oPropertyCollection.ToArray()))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    IItem[] properties = f._Property;
                    ListViewItem item;
                    lvProperty.Items.Clear();
                    foreach (IItem property in properties)
                    {
                        item = new ListViewItem();
                        item.Text = property.Name.ToString();
                        item.ToolTipText = string.Format("{0}({1})", property.Name, property.Unit);
                        item.SubItems.Add(property.Unit.ToString());
                        lvProperty.Items.Add(item);
                    }
                    oPropertyCollection = properties.ToList();
                }

            }
        }
        private void cbCTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            DataGridViewComboBoxColumn cbCol;
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                /*
                 * Get data from database
                 */
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.Connection = conn;
                StringBuilder cmndstring = new StringBuilder();
                //cmndstring.AppendLine("select tp.test_plan_id , t.tool_id, t.tool_name, tp.item_id, tp.item_value, i.item_name, i.item_flag, i.unit, tp.item_value from test_plan tp, tool_sum t, item i");
                //cmndstring.AppendLine("where tp.tool_id = :t and ");
                //cmndstring.AppendLine("tp.tool_id = t.tool_id and tp.item_id = i.item_id and ");
                //cmndstring.AppendLine("(i.item_flag like 'C%') order by tp.test_plan_id, tp.item_id");
                //cmnd.CommandText = cmndstring.ToString();
                cmnd.CommandText = "select * from vw_test_plan where tool_id=:t and item_flag like 'C%'";
                cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                cmnd.Parameters[0].Value = cb.SelectedValue;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                /*
                 * download test plans correspondence with tool and add additional item-custom
                 */
                // compound process
                cbCol = (DataGridViewComboBoxColumn)dataGridView1.Columns["colCMPDProcess"];
                cbCol.ValueMember = "test_plan_id";
                cbCol.DisplayMember = "test_plan_id";
                try
                {
                    DataTable subtbl = dt.Select("item_flag <> 'CT'").CopyToDataTable().DefaultView.ToTable(true, "test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                catch (Exception)
                {
                    DataTable subtbl = new DataTable();
                    subtbl.Columns.Add("test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                // compound temperature
                cbCol = (DataGridViewComboBoxColumn)dataGridView1.Columns["colCMPDTemp"];
                cbCol.ValueMember = "test_plan_id";
                cbCol.DisplayMember = "test_plan_id";

                try
                {
                    DataTable subtbl = dt.Select("item_flag = 'CT'").CopyToDataTable().DefaultView.ToTable(true, "test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                catch (Exception)
                {
                    DataTable subtbl = new DataTable();
                    subtbl.Columns.Add("test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }

                _dtCTestplan = dt; //Save the datatable to local variable                

            }

            //定義 tool 中的 item
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                /*
                 * Get data from database (Define the items of tool)
                 */
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.Connection = conn;
                cmnd.CommandText = "select i.item_id, i.item_name, i.unit , i.item_flag, ti.item_seq_id from item i, tool_sum t, tool_item_info ti " +
                    "where ti.item_id = i.item_id and ti.tool_id = t.tool_id and t.tool_id=:t";
                cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                cmnd.Parameters[0].Value = cb.SelectedValue;

                ITool tool = new Tool();
                tool.ID = cb.SelectedValue.ToString();
                tool.Name = cb.SelectedValue.ToString();
                tool.Flag = "C";

                List<IItem> items = new List<IItem>();
                NpgsqlDataReader reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    IItem item = new Item();
                    item.ID = reader["item_id"].ToString();
                    item.Name = reader["item_name"].ToString();
                    item.Unit = reader["unit"].ToString();
                    item.Flag = reader["item_flag"].ToString();
                    item.Item_Seq = Convert.ToInt32(reader["item_seq_id"]);
                    item.ProdType = "";
                    items.Add(item);
                }
                tool.Items = items.ToArray();
                CompTool = tool;
            }

        }
        private void cbITool_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
             * Get data from database
             */
            ComboBox cb = (ComboBox)sender;
            DataGridViewComboBoxColumn cbCol;
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.Connection = conn;
                StringBuilder cmndstring = new StringBuilder();
                //cmndstring.AppendLine("select tp.test_plan_id , t.tool_id, t.tool_name, tp.item_id, tp.item_value, i.item_name, i.item_flag, i.unit, tp.item_value from test_plan tp, tool_sum t, item i");
                //cmndstring.AppendLine("where tp.tool_id = :t and ");
                //cmndstring.AppendLine("tp.tool_id = t.tool_id and tp.item_id = i.item_id and ");
                //cmndstring.AppendLine("(i.item_flag like 'I%') order by tp.test_plan_id, tp.item_id");
                //cmnd.CommandText = cmndstring.ToString();
                cmnd.CommandText = "select * from vw_test_plan where tool_id=:t and item_flag like 'I%'";
                cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                cmnd.Parameters[0].Value = cb.SelectedValue.ToString();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                /*
                 * download test plans correspondence with tool and add additional item-custom
                 */

                // injection process
                cbCol = (DataGridViewComboBoxColumn)dataGridView1.Columns["colINJProcess"];
                cbCol.ValueMember = "test_plan_id";
                cbCol.DisplayMember = "test_plan_id";
                try
                {
                    DataTable subtbl = dt.Select("item_flag <> 'IT'").CopyToDataTable().DefaultView.ToTable(true, "test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                catch (Exception)
                {
                    DataTable subtbl = new DataTable();
                    subtbl.Columns.Add("test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                // injection temperature
                cbCol = (DataGridViewComboBoxColumn)dataGridView1.Columns["colINJTemp"];
                cbCol.ValueMember = "test_plan_id";
                cbCol.DisplayMember = "test_plan_id";
                try
                {
                    DataTable subtbl = dt.Select("item_flag = 'IT'").CopyToDataTable().DefaultView.ToTable(true, "test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                catch (Exception)
                {
                    DataTable subtbl = new DataTable();
                    subtbl.Columns.Add("test_plan_id");
                    DataRow r = subtbl.NewRow();
                    r["test_plan_id"] = "Custom";
                    subtbl.Rows.Add(r);
                    cbCol.DataSource = subtbl;
                }
                _dtITestplan = dt;
            }

            //定義 tool 中的 item
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                /*
                 * Get data from database
                 */
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.Connection = conn;
                cmnd.CommandText = "select i.item_id, i.item_name, i.unit, i.item_flag, ti.item_seq_id from item i, tool_sum t, tool_item_info ti " +
                    "where ti.item_id = i.item_id and ti.tool_id = t.tool_id and t.tool_id=:t";
                cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                cmnd.Parameters[0].Value = cb.SelectedValue;

                ITool tool = new Tool();
                tool.ID = cb.SelectedValue.ToString();
                tool.Name = cb.SelectedValue.ToString();
                tool.Flag = "I";

                List<IItem> items = new List<IItem>();
                NpgsqlDataReader reader = cmnd.ExecuteReader();
                while (reader.Read())
                {
                    IItem item = new Item();
                    item.ID = reader["item_id"].ToString();
                    item.Name = reader["item_name"].ToString();
                    item.Unit = reader["unit"].ToString();
                    item.Flag = reader["item_flag"].ToString();
                    item.Item_Seq = Convert.ToInt32(reader["item_seq_id"]);
                    items.Add(item);
                }
                tool.Items = items.ToArray();
                InjTool = tool;

                Console.Write("");
            }
            //ComboBox cb = (ComboBox)sender;
            //ITool tool = (ITool)cb.SelectedItem;
            //DataGridViewComboBoxColumn cbCol;
            ////string[] testplan = tool.TestPlanList.Split(';');

            //cbCol = (DataGridViewComboBoxColumn)dataGridView1.Columns["colInj"];
            //cbCol.Items.Clear();
            ////cbCol.Items.AddRange(testplan.Select(x => x).Where(x => !x.Contains("射出溫度")).ToArray());

            //cbCol = (DataGridViewComboBoxColumn)dataGridView1.Columns["colInjTemp"];
            //cbCol.Items.Clear();
            ////testplan = tool.TestPlanList.Split(';');
            ////cbCol.Items.AddRange(testplan.Select(x => x).Where(x => x.Contains("射出溫度")).ToArray());
            //InjTool = tool;
        }
        private void btAdd_Click(object sender, EventArgs e)
        {
            //
            // 競爭者實驗單
            //
            if (ckCompetitor.Checked)
            {
                dataGridView1.Rows.Add();
                return;
            }



            //
            // 一般實驗狀態
            //
            if (oFormulaCollection.Count == 0)
            {
                MessageBox.Show(this, "請先設定配方項目", "建立實驗表", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CompTool.Name))
            {
                MessageBox.Show(this, "請先設定混鍊機台", "建立實驗表", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(InjTool.Name))
            {
                MessageBox.Show(this, "請先設定射出機台", "建立實驗表", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            dataGridView1.Rows.Add();
            int rowid;
            rowid = dataGridView1.RowCount - 1;
            DataGridViewRow row = dataGridView1.Rows[rowid];

            //對測試計畫的四個欄位掛勾預設的測試計畫(cbcell的第一個選項)
            foreach (string colname in new string[] { "colCMPDProcess", "colCMPDTemp", "colINJProcess", "colINJTemp" })
            {
                //colid = dataGridView1.Columns.IndexOf(dataGridView1.Columns[colname]);
                DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)row.Cells[colname];
                if (cbCell.Items.Count > 0)
                {
                    cbCell.Value = ((DataRowView)cbCell.Items[0])["test_plan_id"];

                    string filter = string.Format("test_plan_id='{0}'", cbCell.Value.ToString());
                    DataRow[] dr = null;
                    switch (colname)
                    {
                        case "colCMPDProcess":
                        case "colCMPDTemp":
                            dr = _dtCTestplan.Select(filter);
                            break;
                        case "colINJProcess":
                        case "colINJTemp":
                            dr = _dtITestplan.Select(filter);
                            break;
                        default:
                            break;
                    }
                    IItem item;
                    List<IItem> items = new List<IItem>();
                    foreach (DataRow r in dr)
                    {
                        item = new Item();
                        item.ID = r["item_id"].ToString();
                        item.Name = r["item_name"].ToString();
                        item.Flag = r["item_flag"].ToString();
                        item.ProdType = "";
                        item.Unit = r["unit"].ToString();
                        item.Value = r["item_value"].ToString();
                        //item.Item_Seq = Convert.ToInt32(r["item_seq_id"]); //這裡不需要掛勾 seq_id..因為已經掛在 tool.items 裡面..
                        items.Add(item);
                    }
                    cbCell.Tag = items.ToArray();
                }
            }

        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            DataGridViewCheckBoxCell ckCell;
            for (int i = dataGridView1.RowCount; i-- > 0; )
            {
                ckCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                //ckCell.TrueValue = true;
                if (Convert.ToBoolean(ckCell.Value))
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }

        }
        private void btCopy_Click(object sender, EventArgs e)
        {
            DataGridViewCheckBoxCell ckCell;
            DataGridViewRow clonerow;
            int corrunt_rowcnt = dataGridView1.RowCount;

            for (int i = 0; i < corrunt_rowcnt; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                ckCell = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(ckCell.Value))
                {
                    //clonerow = (DataGridViewRow)row.Clone();
                    dataGridView1.Rows.Add();
                    clonerow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                    for (int j = 0; j < row.Cells.Count; j++)
                    {
                        clonerow.Cells[j].Value = row.Cells[j].Value;
                    }

                    foreach (string colname in new string[] { "colCMPDProcess", "colCMPDTemp", "colINJProcess", "colINJTemp" })
                    {
                        if (row.Cells[colname].Tag != null)
                        {
                            List<IItem> items = new List<IItem>();
                            IItem[] oItems = row.Cells[colname].Tag as IItem[];
                            foreach (IItem item in oItems)
                            {
                                items.Add(item.DeepClon());
                            }
                            clonerow.Cells[colname].Tag = items.ToArray();
                        }
                    }
                    clonerow.Cells[0].Value = false;
                    //dataGridView1.Rows.Add(clonerow);
                }
            }

        }
        private void lvFormula_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if ((e.State & ListViewItemStates.Selected) != 0)
            {
                // Draw the background and focus rectangle for a selected item.                
                //e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                //e.Graphics.DrawRectangle(new Pen(Color.DodgerBlue, 2), e.Bounds);
                //e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
                Rectangle bound = e.Bounds;
                RectangleF rectF = new RectangleF((float)bound.X, (float)bound.Y, (float)bound.Width - 1.0f, (float)bound.Height - 1.0f);
                //e.DrawFocusRectangle();                
                LCY_Database.Tools.DrawingTool.DrawRoundedRectangle(e.Graphics, rectF, 3, new Pen(Color.DodgerBlue, 1));
            }
            //else
            //{
            //    // Draw the background for an unselected item.
            //    using (System.Drawing.Drawing2D.LinearGradientBrush brush =
            //        new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Orange,
            //        Color.Maroon, System.Drawing.Drawing2D.LinearGradientMode.Horizontal))
            //    {
            //        //e.Graphics.FillRectangle(brush, e.Bounds);
            //    }
            //}
            if (((ListView)sender).View != View.Details)
            {
                e.DrawText();
            }
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            //防呆
            //
            // 在非競爭者模式下 袋重和數量必須可轉換成數字
            //
            if (!ckCompetitor.Checked)
            {
                try
                {
                    Convert.ToDouble(textBNumber.Text);
                    Convert.ToDouble(textBWeight.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "在非競爭者模式之下，袋重與數量必須為數字。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //
            // 除了實驗目的和試料牌號均不可空白
            //
            if (string.IsNullOrWhiteSpace(cbUser.Text))
            {
                MessageBox.Show(this, "請輸入申請人資訊。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(cbType.Text))
            {
                MessageBox.Show(this, "請輸入產品類型。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            // 建議日期不可早於申請日期
            //

            if (Convert.ToDateTime(dtPickerApply.Value.ToString("yyyy-M-dd 00:00:00")) >
                Convert.ToDateTime(dtPickerExpDate.Value.ToString("yyyy-M-dd 23:59:59")))
            {
                MessageBox.Show(this, "建議實驗日期不可早於申請日期。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            // 物性不可為空
            //
            if (oPropertyCollection.Count == 0)
            {
                MessageBox.Show(this, "請至少輸入一個物性項目。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //
            // 至少有一個實驗條件
            //
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show(this, "請至少設定一組實驗。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //
            // 實驗表配方欄位內容必須為數字且不為空
            //
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("col_[0-9]+");
            try
            {
                for (int iCol = 0; iCol < dataGridView1.Columns.Count; iCol++)
                {
                    DataGridViewColumn dcol = dataGridView1.Columns[iCol];
                    if (regex.IsMatch(dcol.Name))
                    {
                        for (int iRow = 0; iRow < dataGridView1.Rows.Count; iRow++)
                        {
                            if (dataGridView1[iCol, iRow].Value == null) throw new Exception("配方項設定值不可為空");

                            string data = dataGridView1[iCol, iRow].Value.ToString();
                            if (string.IsNullOrWhiteSpace(data)) throw new Exception("配方項設定值不可為空");
                            Convert.ToDouble(data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is OverflowException)
                {
                    MessageBox.Show(this, "配方項設定值必須為數字格式", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }



            //設定儲存位置
            saveFileDialog.Title = "指定實驗申請單的儲存位置";
            saveFileDialog.Filter = "Excel File|*.xlsx;*.xls";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _excelSavePath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }


            //組出基本的實驗表物件
            ReportBuilder.ExperimentTable expTbl = new ReportBuilder.ExperimentTable();
            expTbl.ApplyDate = dtPickerApply.Value.ToString("yyyy-M-dd");
            expTbl.RunDate = dtPickerExpDate.Value.ToString("yyyy-M-dd");
            expTbl.ProdType = cbType.SelectedValue.ToString();
            expTbl.Grade = textGrade.Text;
            expTbl.User = new KeyValuePair<string, string>(cbUser.SelectedValue.ToString(), ((DataRowView)cbUser.SelectedItem)["emp_ename"].ToString());
            expTbl.BagNumber = Convert.ToDouble(string.IsNullOrWhiteSpace(textBNumber.Text) ? "0" : textBNumber.Text);
            expTbl.BagWeight = Convert.ToDouble(string.IsNullOrWhiteSpace(textBWeight.Text) ? "0" : textBWeight.Text);
            expTbl.Purpose = textPurpose.Text;
            expTbl.Customer = new KeyValuePair<string, string>(cbCustomer.SelectedValue.ToString(), ((DataRowView)cbCustomer.SelectedItem)["customer_cname"].ToString());
            expTbl.Formulas = oFormulaCollection;
            expTbl.Property = oPropertyCollection;
            expTbl.InjTool = InjTool;
            expTbl.CompTool = CompTool;
            //this.Hide();
            if (!backgroundWorker1.IsBusy)
            {
                progressDialog = new FrmProgress() { TopMost = true };
                progressDialog.SetStyle(ProgressBarStyle.Blocks);
                progressDialog.StartPosition = FormStartPosition.CenterScreen;
                progressDialog.Show();

                _dgview = CopyDataGridView(dataGridView1);

                backgroundWorker1.RunWorkerAsync(expTbl);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            //this.Close();

            //MessageBox.Show(expTbl.ToString());

        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox combo = e.Control as ComboBox;
            DataGridView dv = (DataGridView)sender;

            if (combo != null)
            {
                combo.SelectedIndexChanged -= combo_SelectedIndexChanged;
                combo.SelectedIndexChanged += combo_SelectedIndexChanged;
            }
        }
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            //int colid = dataGridView1.CurrentCell.ColumnIndex;
            //int rowid = dataGridView1.CurrentCell.RowIndex;
            string colname = dataGridView1.CurrentCell.OwningColumn.Name;
            DataGridViewComboBoxCell cbCell = dataGridView1.CurrentCell as DataGridViewComboBoxCell;//指向正在執行的 datagridviewComboBox

            /*
             * 這兩行是針對當游標移出原本的 comboboxcell 後，再點擊回來後的"未知"狀況的解法
             * 事件會被觸發多次(不知為何)，而且 sender 內容會不完全~ 直到幾次之後才正常..但若放置不管後面的程式碼會把
             * combox 內的 IItem[] 內容改掉
             */
            if (cb.DataSource == null) return;
            if (cb.DisplayMember == "") return;


            string cbItem = cb.Text;
            if (cbItem != "Custom")//如果不是客製測試計畫，就把對應測試計畫的值載入
            {
                string filter = string.Format("test_plan_id='{0}'", cbItem);
                DataRow[] dr = null;
                switch (colname)
                {
                    case "colCMPDProcess":
                    case "colCMPDTemp":
                        dr = _dtCTestplan.Select(filter);
                        break;
                    case "colINJProcess":
                    case "colINJTemp":
                        dr = _dtITestplan.Select(filter);
                        break;
                    default:
                        break;
                }
                IItem item;
                List<IItem> items = new List<IItem>();
                foreach (DataRow row in dr)
                {
                    item = new Item();
                    item.ID = row["item_id"].ToString();
                    item.Name = row["item_name"].ToString();
                    item.Flag = row["item_flag"].ToString();
                    item.ProdType = "";
                    item.Unit = row["unit"].ToString();
                    item.Value = row["item_value"].ToString();
                    items.Add(item);
                }
                cbCell.Tag = items.ToArray();
                return;
            }

            string sqlString = "";
            switch (colname)
            {
                case "colCMPDProcess":
                    sqlString = string.Format("select i.item_id, i.item_name as Name, i.unit, i.item_flag, ti.tool_id from item i, tool_item_info ti " +
                        "where i.item_id = ti.item_id and tool_id = '{0}' and item_flag {1}'{2}'", CompTool.ID, "!=", "CT");
                    break;
                case "colCMPDTemp":
                    sqlString = string.Format("select i.item_id, i.item_name as Name, i.unit, i.item_flag, ti.tool_id from item i, tool_item_info ti " +
                        "where i.item_id = ti.item_id and tool_id = '{0}' and item_flag {1}'{2}' order by i.item_id", CompTool.ID, "=", "CT");
                    break;
                case "colINJProcess":
                    sqlString = string.Format("select i.item_id, i.item_name as Name, i.unit, i.item_flag, ti.tool_id from item i, tool_item_info ti " +
                        "where i.item_id = ti.item_id and tool_id = '{0}' and item_flag {1}'{2}'", InjTool.ID, "!=", "IT");
                    break;
                case "colINJTemp":
                    sqlString = string.Format("select i.item_id, i.item_name as Name, i.unit, i.item_flag, ti.tool_id from item i, tool_item_info ti " +
                        "where i.item_id = ti.item_id and tool_id = '{0}' and item_flag {1}'{2}' order by i.item_id", InjTool.ID, "=", "IT");
                    break;
                default:
                    break;
            }

            using (FrmCustomEditor f = new FrmCustomEditor(sqlString, cbCell.Tag))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    cbCell.Tag = f.Items.ToArray();
                }
            }


        }
        private void FrmCreateTable_Load(object sender, EventArgs e)
        {
            //Get product type
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select distinct type from item where item_flag like 'P%'";

                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                cbType.ValueMember = "type";
                cbType.DisplayMember = "type";
                cbType.DataSource = dt;
            }
            //Get customer 
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select * from customer order by customer_ename, customer_cname";

                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                cbCustomer.ValueMember = "customer_id";
                cbCustomer.DisplayMember = "customer_cname";
                cbCustomer.DataSource = dt;
            }
            //Get employee
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select * from employee";

                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                cbUser.ValueMember = "emp_id";
                cbUser.DisplayMember = "emp_ename";
                cbUser.DataSource = dt;
            }
            //Download compound tool information 
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select distinct tool_id, tool_name from tool_sum where tool_type='C'";

                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);

                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                cbCTool.ValueMember = "tool_id";
                cbCTool.DisplayMember = "tool_name";
                cbCTool.DataSource = dt;

            }

            //Download injection tool information 
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "select distinct tool_id, tool_name from tool_sum where tool_type='I'";
                cmnd.Connection = conn;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];

                cbITool.ValueMember = "tool_id";
                cbITool.DisplayMember = "tool_name";
                cbITool.DataSource = dt;

            }
        }
        private void ckCompetitor_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            if (ck == null) return;
            if (ck.Checked)
            {
                DataGridViewComboBoxColumn cbCol;
                foreach (string colname in new string[] { "colCMPDProcess", "colCMPDTemp", "colINJProcess", "colINJTemp" })
                {
                    cbCol = dataGridView1.Columns[colname] as DataGridViewComboBoxColumn;
                    cbCol.DataSource = null;
                    cbCol.DisplayMember = "";
                    cbCol.ValueMember = "";
                    cbCol.ReadOnly = true;
                }
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (string colname in new string[] { "colCMPDProcess", "colCMPDTemp", "colINJProcess", "colINJTemp" })
                    {
                        DataGridViewComboBoxCell cbCell = row.Cells[colname] as DataGridViewComboBoxCell;
                        cbCell.Value = null;
                    }
                }
                CompTool = new Tool();
                CompTool.ID = "";
                InjTool = new Tool();
                InjTool.ID = "";
                #region 清空 Formulas
                string[] removecolname = oFormulaCollection.Select(x => "col_" + x.ID).ToArray();
                foreach (string colname in removecolname)
                {
                    int index = dataGridView1.Columns[colname].Index;
                    dataGridView1.Columns.RemoveAt(index);
                }
                lvFormula.Items.Clear();
                oFormulaCollection = new List<IItem>();
                #endregion

                cbCTool.Enabled = false;
                cbITool.Enabled = false;
                btFormulation.Enabled = false;
                btCopy.Enabled = false;


            }
            else
            {
                cbCTool.Enabled = true;
                cbITool.Enabled = true;
                btCopy.Enabled = true;
                btFormulation.Enabled = true;
                DataGridViewComboBoxColumn cbCol;
                foreach (string colname in new string[] { "colCMPDProcess", "colCMPDTemp", "colINJProcess", "colINJTemp" })
                {
                    cbCol = dataGridView1.Columns[colname] as DataGridViewComboBoxColumn;
                    cbCol.ReadOnly = false;
                }
                cbCTool_SelectedIndexChanged(cbCTool, new EventArgs());
                cbITool_SelectedIndexChanged(cbITool, new EventArgs());
                dataGridView1.Rows.Clear();

            }



        }

        #endregion

        #region 方法
        /// <summary>
        /// 判斷 Formula list1 中是否有包含某個Item
        /// 比較的準則是使用 Item ID 去看
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool CompareFormulaCollection(List<IItem> list1, IItem item)
        {
            if (list1 == null || list1.Count == 0) return false;
            if (item == null) return false;

            if (list1.Select((x, i) =>
                               object.Equals(x.ID, item.ID) ? i : -1).Where(x => x != -1).FirstOrDefault() == 0 &&
                list1[0].ID != item.ID)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool SaveDataTableAsCSV(DataTable datatable, string path)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = datatable.Columns.Cast<DataColumn>().
                                  Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in datatable.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }

            try
            {
                System.IO.File.WriteAllText(path, sb.ToString());
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private DataGridView CopyDataGridView(DataGridView dgview1)
        {
            DataGridView dgview2 = new DataGridView();
            foreach (DataGridViewColumn col in dgview1.Columns)
            {
                dgview2.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgview2.AllowUserToAddRows = false;
            DataGridViewRow row;
            foreach (DataGridViewRow dr in dgview1.Rows)
            {
                row = dr.Clone() as DataGridViewRow;
                foreach (DataGridViewCell cell in dr.Cells)
                {
                    row.Cells[cell.ColumnIndex].Value = cell.Value;
                    if (cell.Tag != null) row.Cells[cell.ColumnIndex].Tag = cell.Tag;
                }
                dgview2.Rows.Add(row);
            }

            //dgview2.Rows.RemoveAt(dgview2.Rows.Count - 1);
            return dgview2;
        }


        #endregion

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ReportBuilder.ExperimentTable expTbl = (ReportBuilder.ExperimentTable)e.Argument;

            //計算進度 
            int maxprogress = _dgview.Rows.Count + 1;
            int progress = 0;
            backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "正在建立上傳資料表....");
            #region 建立上傳資料表
            DataTable exp_int = new DataTable();
            string[] col_exp_int = new string[] { "exp_date", "c_tool_id", "i_tool_id", 
                "purpose", "customer_id", "grade", "type", "emp_id", "remark", "item_id", 
                "item_value", "item_flag", "item_name", "unit" };
            foreach (string col in col_exp_int)
            {
                exp_int.Columns.Add(col);
            }
            #endregion
            backgroundWorker1.ReportProgress((progress += 1) * 100 / maxprogress, "建立上傳資料表完成");

            #region 擷取實驗設定與建立表單
            for (int i = 0; i < _dgview.Rows.Count; i++)
            {
                DataGridViewRow row = _dgview.Rows[i];
                backgroundWorker1.ReportProgress((progress * 100) / maxprogress, string.Format("擷取實驗單資料#{0}", row.Index));
                //擷取配方資料
                foreach (IItem item in expTbl.Formulas)
                {
                    item.Value = row.Cells["col_" + item.ID].Value.ToString();
                }
                //擷取機台設定資料
                IItem[] testplan;
                try //考量競爭者實驗單
                {
                    testplan = ((IItem[])row.Cells["colCMPDProcess"].Tag).Union((IItem[])row.Cells["colCMPDTemp"].Tag).ToArray();
                    foreach (IItem item in expTbl.CompTool.Items)
                    {
                        item.Value = testplan.Where(x => x.ID == item.ID).Select(x => x.Value.ToString()).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    testplan = ((IItem[])row.Cells["colINJProcess"].Tag).Union((IItem[])row.Cells["colINJTemp"].Tag).ToArray();
                    foreach (IItem item in expTbl.InjTool.Items)
                    {
                        item.Value = testplan.Where(x => x.ID == item.ID).Select(x => x.Value.ToString()).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                }

                expTbl.Remark = row.Cells["colRemark"].Value == null ? "" : row.Cells["colRemark"].Value.ToString();

                //建立表格
                //每一次試驗上傳的資料列數 (tool items + r items + p items)?
                DataTable dt = expTbl.GetExperimentInitialInfo();
                //exp_int.Merge(dt);
                backgroundWorker1.ReportProgress((progress * 100) / maxprogress, string.Format("上傳實驗數據#{0}至資料庫...", row.Index));

                #region 上傳資料到資料庫--fn_exp_init 一次只能傳一筆
                LCY_DBTools.DBTool.Fn_Upload_Exp_Init(dt);
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    try
                    {
                        NpgsqlCommand cmnd = new NpgsqlCommand("fn_insert_exp_init", conn);
                        cmnd.CommandType = CommandType.StoredProcedure;
                        NpgsqlDataReader reader = cmnd.ExecuteReader();
                        while (reader.Read())
                        {
                            expTbl.SeqID = reader.GetValue(0).ToString();
                        }

                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                #endregion

                backgroundWorker1.ReportProgress((progress * 100) / maxprogress, string.Format("建立申請單#{0}...", row.Index));
                //建立 Excel 表單
                if (_excel == null)
                {
                    _excel = new Excel.Application();
                    _excel.DisplayAlerts = false;
                    _excel.Visible = true;
                    _excel.Workbooks.Add();
                }

                try
                {
                    if (ckCompetitor.Checked)
                    {
                        ReportBuilder.XlReportBuilder.CreateExperimentTable_Competitor(expTbl, _excel);
                    }
                    else
                    {
                        ReportBuilder.XlReportBuilder.CreateExperimentTable(expTbl, _excel);
                    }
                    //ReportBuilder.XlReportBuilder.CreateExperimentUploadTable(expTbl, _excel);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                backgroundWorker1.ReportProgress(((progress += 1) * 100) / maxprogress, string.Format("完成工作#{0}，共{1}筆...", row.Index, _dgview.Rows.Count));
            }

            if (_excel != null)
            {
                //_excel.ActiveWorkbook.SaveAs
                if (_excelSavePath != "")
                {
                    _excel.ActiveWorkbook.SaveAs(Filename: _excelSavePath, FileFormat: Excel.XlFileFormat.xlWorkbookDefault);
                }
                _excel.DisplayAlerts = true;
                _excel = null;
            }

            #endregion

            ////將 datatable 轉存成 csv 檔
            //string fileloc = System.Environment.GetEnvironmentVariable("programdata");
            //fileloc = System.IO.Path.Combine(fileloc, "Minitab", "lcy", "exp_ini.csv");
            //if (!SaveDataTableAsCSV(exp_int, fileloc))
            //{
            //    MessageBox.Show("寫入資料失敗");
            //}

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressDialog.ProgressValue = e.ProgressPercentage;
            progressDialog.autoReport_progressChange(sender, e);
            System.Windows.Forms.Application.DoEvents();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {

            }
            else if (e.Error != null)
            {
                //progressDialog.Message = "錯誤: " + e.Error.Message;                
                MessageBox.Show(progressDialog, "錯誤: " + e.Error.Message, "",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                progressDialog.Close();
            }
            else
            {

                MessageBox.Show(progressDialog, "報表建立完成", "",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                progressDialog.Close();

            }
            //this.Dispose();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void linkLab_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmLogon f = new FrmLogon())
            {
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshDialogByLogonStatus(f.GetUserType());
                    _userType = f.GetUserType();
                }
            }
        }
    }
}
