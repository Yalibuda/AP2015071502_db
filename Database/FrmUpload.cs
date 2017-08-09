using LCY_Database.ReportBuilder;
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
using Excel = Microsoft.Office.Interop.Excel;

namespace LCY_Database
{
    public partial class FrmUpload : Form
    {
        public FrmUpload()
        {
            InitializeComponent();
        }

        public FrmUpload(UserType userType)
        {
            InitializeComponent();
            RefreshDialogByLogonStatus(userType);
            _userType = userType;
        }


        private UserType _userType = UserType.GUEST;
        private string tool_id = "";
        private string item_flags_sqlstirng = "";
        private string test_plan_id = "";
        private string _filePath_tool = "";
        private string _filePath_items = "";
        private string _filePath_result = "";
        private string _filePath_customer = "";
        private FrmProgress progressDialog;
        private string _connString = LCY_DBTools.DBTool.GetConnString();

        #region 事件
        /**************************
         * Tool  
         * 
         */
        private void btDownloadSample_Tool_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "選擇儲存路徑";
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "";
            string savefilepath = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savefilepath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }
            DataTable dt;
            if (cbPurpose_Tool.SelectedValue.ToString() == "1")
            {
                string[] colname = new string[] { "tool_name", "tool_type", "item_name", "item_flag", "unit", "value", "test_plan", "item_seq_id" };
                dt = LCY_DBTools.DBTool.Create_Blank_Table(colname);
            }
            else
            {
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        using (NpgsqlDataAdapter da = new NpgsqlDataAdapter())
                        {
                            StringBuilder cmndString = new StringBuilder();
                            cmndString.AppendLine("(select a.tool_name, a.tool_type,b.item_name,b.item_flag,b.unit, null as value, null test_plan, c.item_seq_id from tool_sum a, item b, tool_item_info c");
                            cmndString.AppendLine("where c.tool_id=a.tool_id and c.item_id = b.item_id and c.tool_id=:t order by c.item_seq_id)");
                            cmndString.AppendLine("UNION ALL");
                            cmndString.AppendLine("(select a.tool_name, a.tool_type,b.item_name,b.item_flag,b.unit, c.item_value as value ,c.test_plan_id as test_plan, d.item_seq_id ");
                            cmndString.AppendLine("from tool_sum a, item b, test_plan c, tool_item_info d");
                            cmndString.AppendLine("where c.tool_id=a.tool_id and c.item_id = b.item_id and d.item_id=c.item_id and d.tool_id=a.tool_id and c.tool_id=:t");
                            cmndString.AppendLine("order by c.test_plan_id,d.item_seq_id)");
                            da.SelectCommand = new NpgsqlCommand(cmndString.ToString(), conn);
                            da.SelectCommand.Parameters.Add("t", NpgsqlTypes.NpgsqlDbType.Char);
                            da.SelectCommand.Parameters["t"].SourceColumn = "tool_id";
                            da.SelectCommand.Parameters["t"].Value = cbToolName_Tool.SelectedValue.ToString();
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //將 datatable 轉存成 csv 檔            
            if (!LCY_DBTools.DBTool.SaveDataTableAsCSV(dt, savefilepath))
            {
                MessageBox.Show("寫入資料失敗");
            }
            else
            {
                MessageBox.Show(null, string.Format("範例檔下載完成。請至\r\n{0}，進行編輯後上傳。", savefilepath),
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }
        private void btBrowse_Tool_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "開啟機台資訊檔案";
            openFileDialog.Filter = "CSV|*.csv";
            string path = textUploadPath_Tool.Text;
            if (path != string.Empty && System.IO.Directory.Exists(System.IO.Directory.GetParent(path).ToString()))
            {
                openFileDialog.InitialDirectory = System.IO.Directory.GetParent(path).ToString();
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _filePath_tool = openFileDialog.FileName;
                textUploadPath_Tool.Text = _filePath_tool;
            }
            else
            {
                return;
            }
        }
        private void btOK_Tool_Click(object sender, EventArgs e)
        {
            //防呆1
            #region 確認檔案和警語
            if (string.IsNullOrWhiteSpace(_filePath_tool))
            {
                MessageBox.Show(null, "請指定檔案路徑", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.IO.File.Exists(_filePath_tool))
            {
                MessageBox.Show(null, "檔案不存在，請確認路徑是否正確。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (cbPurpose_Tool.SelectedValue.ToString())
            {
                case "1":

                    break;
                case "2":
                    if (MessageBox.Show(null, "異動現有機台資訊會刪除所有測試計畫內容，您將需要重新設定計畫。\r\n確定要繼續執行?",
                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.Yes)
                    {
                    }
                    else
                    {
                        return;
                    }
                    break;
                default:
                    break;
            }

            #endregion

            try
            {
                LCY_DBTools.DBTool.Fn_Upload_Test_Plan(_filePath_tool);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //
            // 防呆2
            // 確認 upload_test_plan 裡面的 tool_name
            //
            string uploadedToolId = "";
            #region 確認上傳資料內容和資料庫能否對應
            try
            {
                //確認只上傳一個機台
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select distinct tool_name from upload_test_plan", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Select("tool_name is null") != null && dt.Select("tool_name is null").Count() > 0)
                    {
                        MessageBox.Show(null, string.Format("檔案中有資料未給定機台名稱", dt.Rows.Count),
                            "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dt.Rows.Count >= 2)
                    {
                        MessageBox.Show(null, string.Format("檔案中包含{0}個機台資訊，一次只能上傳一個機台資料。", dt.Rows.Count),
                            "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        uploadedToolId = dt.Rows[0]["tool_name"].ToString();
                    }
                }
                //確認 DB 中是否有對應的機台
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select distinct a.tool_name from tool_sum a, upload_test_plan b where a.tool_name = b.tool_name ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (cbPurpose_Tool.SelectedValue.ToString() == "1")
                    {
                        //如果有對應的名字要退出
                        if (dt.Rows.Count >= 1)
                        {
                            MessageBox.Show(null, "資料庫中已有相同名稱的機台，請使用編輯模式。",
                            "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //如果沒對應的名字要退出
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show(null, "資料庫中找不到對應的機台，請使用確認機台名稱是否正確或新增機台。",
                            "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(null,
                          "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                          "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion

            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                try
                {
                    NpgsqlCommand cmnd;
                    //如果是編輯機台，需要把相關的 test_plan 和 tool_item_info 刪除
                    if (cbPurpose_Tool.SelectedValue.ToString() == "2")
                    {
                        cmnd = new NpgsqlCommand("delete from test_plan where tool_id=:t", conn);
                        cmnd.Parameters.Add("t", NpgsqlTypes.NpgsqlDbType.Char);
                        cmnd.Parameters[0].SourceColumn = "tool_id";
                        cmnd.Parameters[0].Value = uploadedToolId;
                        cmnd.ExecuteNonQuery();

                        cmnd.CommandText = "delete from tool_item_info where tool_id=:t";
                        cmnd.ExecuteNonQuery();

                    }

                    cmnd = new NpgsqlCommand("fn_insert_tool_sum", conn);
                    cmnd.CommandType = CommandType.StoredProcedure;
                    cmnd.ExecuteNonQuery();
                    cmnd.CommandText = "fn_insert_item";
                    cmnd.ExecuteNonQuery();
                    cmnd.CommandText = "fn_insert_tool_item_info";
                    cmnd.ExecuteNonQuery();
                    cmnd.CommandText = "fn_insert_test_plan";
                    cmnd.ExecuteNonQuery();
                    cmnd.CommandText = " fn_update_test_plan_list";
                    cmnd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(null,
                           "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                           "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    MessageBox.Show(null,
                        "上傳程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }

        }
        private void btCancel_Tool_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void cbPurpose_Tool_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb == null) return;
            switch (cb.SelectedValue.ToString())
            {
                case "1": //Add                    
                    labToolName_Tool.Visible = false;
                    cbToolName_Tool.Visible = false;
                    break;
                case "2": //Edit
                    labToolName_Tool.Visible = true;
                    cbToolName_Tool.Visible = true;
                    break;
                default:
                    break;
            }
            RefreshToolQuery();
        }
        private DataTable QueryTools()
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("select distinct tool_id, tool_name, tool_type from tool_sum order by tool_type, tool_name", conn))
                    {
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void RefreshToolQuery()
        {
            if (cbPurpose_Tool.SelectedValue.ToString() == "2")
            {
                DataTable dt = QueryTools();
                cbToolName_Tool.ValueMember = "tool_id";
                cbToolName_Tool.DisplayMember = "tool_name";
                cbToolName_Tool.DataSource = dt;
            }
            else
            {
                cbToolName_Tool.DataSource = null;
            }
        }

        /**************************
         * Test plan
         * 
         */
        private void cbToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.Connection = conn;
                cmnd.CommandText = "select tool_id, tool_name from tool_sum where tool_type=:t";
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
                cbToolName_Plan.ValueMember = "tool_id";
                cbToolName_Plan.DisplayMember = "tool_name";
                cbToolName_Plan.DataSource = dt;
            }

            //
            // 設定參數類型
            //
            DataTable item_flag = new DataTable();
            string[] col_tool_type = new string[] { "item_flag_id", "item_flag_group" };
            foreach (string colname in col_tool_type)
            {
                item_flag.Columns.Add(colname);
            }
            item_flag.Rows.Add(cb.SelectedValue, "加工條件");
            item_flag.Rows.Add(cb.SelectedValue + "T", "區段溫度");
            cbCondition_Plan.ValueMember = "item_flag_id";
            cbCondition_Plan.DisplayMember = "item_flag_group";
            cbCondition_Plan.DataSource = item_flag;

        }
        private void btPlanRead_Click(object sender, EventArgs e)
        {
            //
            // 防呆
            //
            if (cbToolName_Plan.SelectedItem == null ||
                string.IsNullOrEmpty(((DataRowView)cbToolName_Plan.SelectedItem)["tool_name"].ToString()))
            {
                MessageBox.Show(null, "機台名稱不可為空", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string desc = "";
            desc = ((DataRowView)cbToolName_Plan.SelectedItem)["tool_name"].ToString() + Environment.NewLine +
                ((DataRowView)cbCondition_Plan.SelectedItem)["item_flag_group"].ToString();

            tool_id = cbToolName_Plan.SelectedValue.ToString();
            string condition = cbCondition_Plan.SelectedValue.ToString();
            switch (condition)
            {
                case "CT":
                    item_flags_sqlstirng = "i.item_flag='CT'";
                    break;
                case "C":
                    item_flags_sqlstirng = "i.item_flag!='CT'";
                    break;
                case "IT":
                    item_flags_sqlstirng = "i.item_flag='IT'";
                    break;
                case "I":
                    item_flags_sqlstirng = "i.item_flag!='IT'";
                    break;
                default:
                    item_flags_sqlstirng = "";
                    break;
            }
            labTestPlan.Text = desc;
            cbTestPlanEdit_SelectedIndexChanged(cbTestPlanEdit, new EventArgs());
        }
        private void cbTestPlanEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (tool_id == "" || item_flags_sqlstirng == "")
            {
                MessageBox.Show(this, "請先設定機台與參數類型", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (cb.SelectedValue.ToString())
            {
                case "1"://Add
                    btOK_Plan.Text = "新增";
                    textTestPlanName.Text = string.Empty;
                    cbSelTestPlan.Visible = false;
                    textTestPlanName.Visible = true;
                    //textTestPlanName.Location = new Point(365, 53);
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        StringBuilder cmndText = new StringBuilder();
                        conn.Open();
                        NpgsqlCommand cmnd = new NpgsqlCommand();
                        cmnd.Connection = conn;
                        cmndText.AppendLine("select i.item_id, i.item_name, null as value, i.unit, t.tool_id, t.tool_name, null test_plan, t.tool_type, i.item_flag, ti.item_seq_id " +
                            "from item i, tool_sum t, tool_item_info ti");
                        cmndText.AppendLine("where i.item_id=ti.item_id and ti.tool_id = t.tool_id and t.tool_id=:t");
                        cmndText.AppendLine(string.Format("and {0}", item_flags_sqlstirng));
                        cmndText.AppendLine("order by i.item_id");
                        cmnd.CommandText = cmndText.ToString();
                        cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                        cmnd.Parameters[0].Value = tool_id;
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        dataGridViewTestPlan.DataSource = dt;

                        //調整 datagridview 外觀                        
                        dataGridViewTestPlan.Columns["item_name"].HeaderText = "Name";
                        dataGridViewTestPlan.Columns["item_name"].ReadOnly = true;
                        dataGridViewTestPlan.Columns["item_name"].Width = 100;
                        dataGridViewTestPlan.Columns["value"].HeaderText = "Value";
                        dataGridViewTestPlan.Columns["value"].ReadOnly = false;
                        dataGridViewTestPlan.Columns["value"].Width = 50;
                        dataGridViewTestPlan.Columns["test_plan"].HeaderText = "Plan";
                        dataGridViewTestPlan.Columns["test_plan"].Width = 50;
                        dataGridViewTestPlan.Columns["unit"].HeaderText = "Unit";
                        dataGridViewTestPlan.Columns["unit"].MinimumWidth = 50;
                        dataGridViewTestPlan.Columns["unit"].ReadOnly = true;
                        dataGridViewTestPlan.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        dataGridViewTestPlan.Columns["item_id"].Visible = false;
                        dataGridViewTestPlan.Columns["tool_id"].Visible = false;
                        dataGridViewTestPlan.Columns["tool_name"].Visible = false;
                        dataGridViewTestPlan.Columns["tool_type"].Visible = false;
                        dataGridViewTestPlan.Columns["test_plan"].Visible = false;
                        dataGridViewTestPlan.Columns["item_flag"].Visible = false;
                        dataGridViewTestPlan.Columns["item_seq_id"].Visible = false;
                    }
                    break;
                case "2"://Edit
                case "3"://Remove
                    if (cb.SelectedValue.ToString() == "2")
                        btOK_Plan.Text = "修改";
                    else
                        btOK_Plan.Text = "刪除";
                    cbSelTestPlan.Visible = true;
                    textTestPlanName.Visible = false;
                    //textTestPlanName.Location = new Point(365, 53);
                    dataGridViewTestPlan.DataSource = null;
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        StringBuilder cmndText = new StringBuilder();
                        conn.Open();
                        NpgsqlCommand cmnd = new NpgsqlCommand();
                        cmnd.Connection = conn;
                        cmndText.Clear();
                        cmndText.AppendLine("select distinct tp.test_plan_id from item i, test_plan tp");
                        cmndText.AppendLine("where i.item_id=tp.item_id and tp.tool_id=:t");
                        cmndText.AppendLine(string.Format("and {0}", item_flags_sqlstirng));
                        cmndText.AppendLine("order by tp.test_plan_id");
                        cmnd.CommandText = cmndText.ToString();
                        cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                        cmnd.Parameters[0].Value = tool_id;
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        cbSelTestPlan.ValueMember = "test_plan_id";
                        cbSelTestPlan.DisplayMember = "test_plan_id";
                        cbSelTestPlan.DataSource = dt;
                    }

                    break;
                default:
                    break;
            }

        }
        private void cbSelTestPlan_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            test_plan_id = cb.SelectedValue.ToString();
            if (tool_id == "" || item_flags_sqlstirng == "" || test_plan_id == "")
            {
                MessageBox.Show(this, "請先設定機台、參數類型和測試計畫", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                StringBuilder cmndText = new StringBuilder();
                conn.Open();
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.Connection = conn;
                cmndText.AppendLine("select tp.item_id, i.item_name, tp.item_value as value, i.unit, tp.tool_id, t.tool_name, tp.test_plan_id as test_plan, t.tool_type, i.item_flag , ti.item_seq_id " +
                    "from test_plan tp, item i, tool_sum t, tool_item_info ti");
                cmndText.AppendLine("where tp.item_id = i.item_id and tp.tool_id=t.tool_id and tp.item_id = ti.item_id and ti.tool_id = t.tool_id and tp.tool_id=:t and tp.test_plan_id=:tp");
                cmndText.AppendLine("order by ti.item_seq_id");
                cmnd.CommandText = cmndText.ToString();
                cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                cmnd.Parameters.Add(new NpgsqlParameter("tp", DbType.String));
                cmnd.Parameters[0].Value = tool_id;
                cmnd.Parameters[1].Value = test_plan_id;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmnd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable dt = ds.Tables[0];
                dataGridViewTestPlan.DataSource = dt;
            }
            //調整 datagridview 外觀
            dataGridViewTestPlan.Columns["item_name"].HeaderText = "Name";
            dataGridViewTestPlan.Columns["item_name"].ReadOnly = true;
            dataGridViewTestPlan.Columns["item_name"].Width = 100;
            dataGridViewTestPlan.Columns["value"].HeaderText = "Value";
            dataGridViewTestPlan.Columns["value"].ReadOnly = false;
            dataGridViewTestPlan.Columns["value"].Width = 50;
            dataGridViewTestPlan.Columns["unit"].HeaderText = "Unit";
            dataGridViewTestPlan.Columns["unit"].ReadOnly = true;
            dataGridViewTestPlan.Columns["unit"].MinimumWidth = 50;
            dataGridViewTestPlan.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewTestPlan.Columns["item_id"].Visible = false;
            dataGridViewTestPlan.Columns["tool_id"].Visible = false;
            dataGridViewTestPlan.Columns["tool_name"].Visible = false;
            dataGridViewTestPlan.Columns["tool_type"].Visible = false;
            dataGridViewTestPlan.Columns["test_plan"].Visible = false;
            dataGridViewTestPlan.Columns["item_flag"].Visible = false;
            dataGridViewTestPlan.Columns["item_seq_id"].Visible = false;

            if (cbTestPlanEdit.SelectedValue.ToString() == "3")
            {
                dataGridViewTestPlan.Columns["value"].ReadOnly = true;
            }
        }
        private void btOK_Plan_Click(object sender, EventArgs e)
        {
            //
            // 事前防呆
            //
            switch (cbTestPlanEdit.SelectedValue.ToString())
            {
                case "1":
                    foreach (DataGridViewRow row in dataGridViewTestPlan.Rows)
                    {
                        if (string.IsNullOrWhiteSpace(row.Cells["value"].Value.ToString()))
                        {
                            MessageBox.Show(null, "測試計畫內容不可為空", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        //檢查是否有重複的名稱
                        using (NpgsqlCommand cmnd = new NpgsqlCommand("select * from test_plan where test_plan_id=:tp and tool_id=:t limit 1", conn))
                        {

                            cmnd.Parameters.Add(new NpgsqlParameter("tp", DbType.String));
                            cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                            cmnd.Parameters[0].Value = textTestPlanName.Text;
                            cmnd.Parameters[1].Value = tool_id;
                            if (cmnd.ExecuteReader().HasRows)
                            {
                                MessageBox.Show(null, string.Format("{0}內已有相同名稱的測試計畫，請使用其他名稱。", cbToolName_Plan.FormatString), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        //執行上傳函數
                        using (NpgsqlCommand cmnd = new NpgsqlCommand("fn_insert_test_plan", conn))
                        {
                            try
                            {
                                //Get data to datatable
                                DataTable dt = dataGridViewTestPlan.DataSource as DataTable;
                                LCY_DBTools.DBTool.Fn_Upload_Test_Plan(dt);
                                cmnd.CommandType = CommandType.StoredProcedure;
                                cmnd.ExecuteNonQuery();
                                cmnd.CommandText = "fn_update_test_plan_list";
                                cmnd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(null,
                                       "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                                       "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                MessageBox.Show(null,
                                    "上傳程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                conn.Close();
                            }
                        }
                    }
                    break;
                case "2":
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        //執行上傳函數
                        using (NpgsqlCommand cmnd = new NpgsqlCommand("fn_insert_test_plan", conn))
                        {
                            try
                            {
                                //Get data to datatable
                                DataTable dt = dataGridViewTestPlan.DataSource as DataTable;
                                LCY_DBTools.DBTool.Fn_Upload_Test_Plan(dt);
                                cmnd.CommandType = CommandType.StoredProcedure;
                                cmnd.ExecuteNonQuery();
                                cmnd.CommandText = "fn_update_test_plan_list";
                                cmnd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(null,
                                       "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                                       "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            finally
                            {
                                MessageBox.Show(null,
                                    "上傳程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                conn.Close();
                            }
                        }
                    }
                    break;
                case "3":

                    if (MessageBox.Show(this,
                        string.Format("確定要刪除測試計畫 {0}", cbSelTestPlan.Text), "",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        try
                        {
                            conn.Open();
                            NpgsqlCommand cmnd = new NpgsqlCommand("delete from test_plan where test_plan_id=:tp and tool_id=:t", conn);
                            cmnd.Parameters.Add(new NpgsqlParameter("tp", DbType.String));
                            cmnd.Parameters.Add(new NpgsqlParameter("t", DbType.String));
                            cmnd.Parameters[0].Value = cbSelTestPlan.Text;
                            cmnd.Parameters[1].Value = tool_id;
                            cmnd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(null,
                                           "刪除時時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                                           "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        finally
                        {
                            MessageBox.Show(null,
                                        "刪除程序程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            conn.Close();
                            //cbTestPlanEdit.SelectedIndex = 2;
                            cbTestPlanEdit_SelectedIndexChanged(cbTestPlanEdit, new EventArgs());
                        }
                    }

                    break;
                default:
                    break;
            }


        }
        private void btCancel_Plan_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void textTestPlanName_TextChanged(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            string testplan = "";
            if (txtbox != null)
            {
                testplan = txtbox.Text;
            }
            else
            {
                return;
            }
            if (dataGridViewTestPlan.DataSource != null && cbTestPlanEdit.SelectedValue.ToString() == "1")
            {
                DataTable dt = dataGridViewTestPlan.DataSource as DataTable;
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr["test_plan"] = testplan;
                    }
                }

            }
        }

        /**************************
         * Items
         * 
         */
        private void btBrowse_Item_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = string.Format("開啟{0}資訊檔案", (rbtRItem.Checked ? "配方" : "物性"));
            openFileDialog.Filter = "CSV|*.csv";
            string path = textUploadPath_Item.Text;
            if (path != string.Empty && System.IO.Directory.Exists(System.IO.Directory.GetParent(path).ToString()))
            {
                openFileDialog.InitialDirectory = System.IO.Directory.GetParent(path).ToString();
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _filePath_items = openFileDialog.FileName;
                textUploadPath_Item.Text = _filePath_items;
            }
            else
            {
                return;
            }
        }
        private void btOK_item_Click(object sender, EventArgs e)
        {
            try
            {
                switch (cbPurpose_Item.SelectedValue.ToString())
                {
                    case "1":
                        UploadItemToDB();
                        break;
                    case "2":
                        UpdateItemOnDB();
                        MessageBox.Show(this, "編輯完成", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshItemQuery();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
        private void btCancel_Item_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void cbPurpose_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (sender == null) return;
            switch (cb.SelectedValue.ToString())
            {
                case "1"://上傳
                    labStep1_Item.Visible = true;
                    labStep2_Item.Visible = true;
                    textUploadPath_Item.Visible = true;
                    btDownloadSampe_Item.Visible = true;
                    btBrowse_Items.Visible = true;
                    btOK_item.Text = "上傳";
                    dataGridView_Item.Visible = false;
                    dataGridView_Item.DataSource = null;
                    break;
                case "2"://編輯
                    labStep1_Item.Visible = false;
                    labStep2_Item.Visible = false;
                    textUploadPath_Item.Visible = false;
                    btDownloadSampe_Item.Visible = false;
                    btBrowse_Items.Visible = false;
                    btOK_item.Text = "確定";
                    dataGridView_Item.Visible = true;
                    break;
            }
            RefreshItemQuery();

        }
        private void btDownloadSampe_Item_Click(object sender, EventArgs e)
        {
            saveFileDialog.Title = "選擇儲存路徑";
            saveFileDialog.Filter = "CSV|*.csv";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "";
            string savefilepath = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savefilepath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            //string[] colname;
            //if (rbtRItem.Checked)
            //{
            //    colname = new string[] { "r_name", "unit" };
            //}
            //else
            //{
            //    colname = new string[] { "p_name", "type", "unit" };
            //}
            //DataTable dt = LCY_Database.DBTool.Create_Blank_Table(colname);

            //下載資料庫中所有配方/物性資料
            DataTable dt = new DataTable();
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter())
                    {
                        string cmndText = "";
                        if (rbtRItem.Checked)
                        {
                            cmndText = "select item_name as r_name, unit from item where item_flag like 'R%' order by item_name";
                        }
                        else
                        {
                            cmndText = "select item_name as p_name, type, unit from item where item_flag like 'P%' order by type, item_name";
                        }
                        da.SelectCommand = new NpgsqlCommand(cmndText, conn);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //將 datatable 轉存成 csv 檔            
            if (!LCY_DBTools.DBTool.SaveDataTableAsCSV(dt, savefilepath))
            {
                MessageBox.Show("寫入資料失敗");
            }
            else
            {
                MessageBox.Show(null, string.Format("範例檔下載完成。請至\r\n{0}，進行編輯後上傳。", savefilepath),
                    "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        private void rbtRItem_CheckedChanged(object sender, EventArgs e)
        {
            RefreshItemQuery();
        }
        private void rbtTItem_CheckedChanged(object sender, EventArgs e)
        {
            RefreshItemQuery();
        }
        private DataTable QueryItems(string item_flag)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter("select item_id, item_name, unit, type from item where item_flag like :f order by type, item_name, unit", conn))
                    {
                        da.SelectCommand.Parameters.Add("f", NpgsqlTypes.NpgsqlDbType.Char);
                        da.SelectCommand.Parameters[0].SourceColumn = "item_flag";
                        da.SelectCommand.Parameters[0].Value = item_flag;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        return dt;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void RefreshItemQuery()
        {
            if (cbPurpose_Item.SelectedValue.ToString() == "2")
            {
                string para;
                if (rbtRItem.Checked)
                {
                    para = "R%";
                }
                else
                {
                    para = "P%";
                }
                DataTable dt = QueryItems(para);
                dataGridView_Item.DataSource = dt;
                dataGridView_Item.Columns["item_name"].HeaderText = "Name";
                dataGridView_Item.Columns["item_name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView_Item.Columns["unit"].HeaderText = "Unit";
                dataGridView_Item.Columns["unit"].MinimumWidth = 50;
                dataGridView_Item.Columns["unit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView_Item.Columns["item_id"].Visible = false;
                dataGridView_Item.Columns["type"].MinimumWidth = 50;
                dataGridView_Item.Columns["type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (rbtRItem.Checked)
                {
                    dataGridView_Item.Columns["type"].Visible = false;
                }
                else
                {
                    dataGridView_Item.Columns["type"].Visible = true;
                }

            }
        }
        private void UploadItemToDB()
        {
            //防呆
            if (string.IsNullOrWhiteSpace(_filePath_items))
            {
                MessageBox.Show(null, "請指定檔案路徑", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.IO.File.Exists(_filePath_items))
            {
                MessageBox.Show(null, "檔案不存在，請確認路徑是否正確。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //上傳資料至 upload table
            try
            {
                if (rbtRItem.Checked)
                {
                    LCY_DBTools.DBTool.Fn_Upload_R_Item(_filePath_items);
                }
                else if (rbtTItem.Checked)
                {
                    LCY_DBTools.DBTool.Fn_Upload_P_Item(_filePath_items);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(null,
                       "上傳 upload table 時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                       "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //執行 fn
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                int count_item = 0;
                try
                {
                    NpgsqlCommand cmnd = new NpgsqlCommand();
                    cmnd.Connection = conn;
                    cmnd.CommandType = CommandType.StoredProcedure;
                    if (rbtRItem.Checked)
                    {
                        cmnd.CommandText = "fn_insert_r_item";
                        count_item = cmnd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmnd.CommandText = "fn_insert_p_item";
                        count_item = cmnd.ExecuteNonQuery();
                    }
                }
                catch (ArgumentNullException augNullEx)
                {
                    MessageBox.Show(null,
                          augNullEx.Message,
                          "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(null,
                           "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                           "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                
                finally
                {
                    MessageBox.Show(null,
                        "上傳程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
        }
        private void UpdateItemOnDB()
        {
            DataTable dt = dataGridView_Item.DataSource as DataTable;
            if (dt == null)
            {
                MessageBox.Show(null, "無資料來源，更新動作將被忽略", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dt_update = dt.GetChanges();
            if (dt_update == null) return;

            try
            {
                NpgsqlCommand cmnd = new NpgsqlCommand();
                cmnd.CommandText = "update item set (item_name, unit, type, rpt_date_time)=(:name, :unit, :type, now()) where item_id=:id;" +
                    "update exp_detail set (item_name, unit)=(:name, :unit) where item_id=:id;";
                cmnd.Parameters.Add("name", NpgsqlTypes.NpgsqlDbType.Char);
                cmnd.Parameters.Add("unit", NpgsqlTypes.NpgsqlDbType.Char);
                cmnd.Parameters.Add("type", NpgsqlTypes.NpgsqlDbType.Char);
                cmnd.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Integer);
                cmnd.Parameters["name"].SourceColumn = "item_name";
                cmnd.Parameters["unit"].SourceColumn = "unit";
                cmnd.Parameters["type"].SourceColumn = "type";
                cmnd.Parameters["id"].SourceColumn = "item_id";

                foreach (DataRow dr in dt_update.Rows)
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        cmnd.Connection = conn;
                        if (dr["item_name"] == DBNull.Value | dr["unit"] == DBNull.Value)
                        {
                            throw new ArgumentNullException("項目名稱/單位不可為空白");
                        }
                        cmnd.Parameters["name"].Value = dr["item_name"];
                        cmnd.Parameters["unit"].Value = dr["unit"];
                        cmnd.Parameters["type"].Value = dr["type"];
                        cmnd.Parameters["id"].Value = dr["item_id"];
                        cmnd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /**************************
         * Result
         * 
         */
        private void btSetSeqId_Result_Click(object sender, EventArgs e)
        {
            string[] selectedSeqId = null;
            if (lvSelectedSeqID.Items.Count > 0)
            {
                selectedSeqId = lvSelectedSeqID.Items.Cast<ListViewItem>().Select(x => x.Text).ToArray();
            }
            using (FrmSelectSeq f = new FrmSelectSeq(selectedSeqId))
            {
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    lvSelectedSeqID.BeginUpdate();
                    lvSelectedSeqID.Items.Clear();
                    foreach (string item in f.GetSelectedItems())
                    {
                        lvSelectedSeqID.Items.Add(new ListViewItem(item));
                    }
                    lvSelectedSeqID.EndUpdate();
                }
            }
        }
        private void btDownloadSample_Result_Click(object sender, EventArgs e)
        {
            //
            // 防呆
            //

            // 確認是否有 seq_id
            string[] seqIdArray;
            if (lvSelectedSeqID.Items.Count == 0)
            {
                MessageBox.Show(this, "請於步驟一中至少選擇一組下載的實驗序號", "",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Warning);
                return;
            }
            else
            {
                seqIdArray = lvSelectedSeqID.Items.Cast<ListViewItem>().Select(x => x.Text).ToArray();
            }

            //設定儲存路徑
            saveFileDialog.Title = "選擇儲存路徑";
            saveFileDialog.Filter = "Excel|*.xlsx;*.xls";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "";
            string savefilepath = "";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                savefilepath = saveFileDialog.FileName;
            }
            else
            {
                return;
            }

            if (!backgroundWorker2.IsBusy)
            {
                progressDialog = new FrmProgress() { TopMost = true };
                progressDialog.SetStyle(ProgressBarStyle.Blocks);
                progressDialog.StartPosition = FormStartPosition.CenterScreen;
                progressDialog.Show();
                BgWorkerArgs bkArg = new BgWorkerArgs();
                bkArg.FilePath = savefilepath;
                bkArg.SeqIdArray = seqIdArray;
                try
                {
                    backgroundWorker2.RunWorkerAsync(bkArg);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

        }
        private void btBrowse_Result_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "開啟實驗上傳表";
            openFileDialog.Filter = "Excel|*.xls;*.xlsx";
            string path = textUploadPath_Result.Text;
            if (path != string.Empty && System.IO.Directory.Exists(System.IO.Directory.GetParent(path).ToString()))
            {
                openFileDialog.InitialDirectory = System.IO.Directory.GetParent(path).ToString();
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _filePath_result = openFileDialog.FileName;
                textUploadPath_Result.Text = _filePath_result;
            }
            else
            {
                return;
            }
        }
        private void btOK_result_Click(object sender, EventArgs e)
        {
            //
            // 防呆
            //
            if (string.IsNullOrWhiteSpace(_filePath_result))
            {
                MessageBox.Show(null, "請指定檔案路徑", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.IO.File.Exists(_filePath_result))
            {
                MessageBox.Show(null, "檔案不存在，請確認路徑是否正確。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!backgroundWorker1.IsBusy)
            {
                progressDialog = new FrmProgress() { TopMost = true };
                progressDialog.SetStyle(ProgressBarStyle.Blocks);
                progressDialog.StartPosition = FormStartPosition.CenterScreen;
                progressDialog.Show();
                try
                {
                    backgroundWorker1.RunWorkerAsync(_filePath_result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }

            #region 移動到 backgroundworker
            ////
            //// 將 Excel 內的 sheet 轉存成 csv 並取出 remark
            ////
            //Excel.Application xlApp = new Excel.Application();
            //xlApp.Visible = false;
            //xlApp.DisplayAlerts = false;
            //xlApp.Workbooks.Open(_filePath_result);
            //string path = Environment.GetEnvironmentVariable("allusersprofile");
            //path = System.IO.Path.Combine(path, "Minitab", "lcy");
            //string filepath;
            //Excel.Workbook wbook = xlApp.ActiveWorkbook;
            //Dictionary<string, string> remarkinfo = new Dictionary<string, string>();
            ////
            //// 將 Excel 拆解成多份 csv 檔
            ////
            //StringBuilder iniContent = new StringBuilder();
            //foreach (Excel._Worksheet wsheet in wbook.Worksheets)
            //{
            //    if (wsheet.Name.EndsWith("_Upload"))
            //    {
            //        wsheet.Activate();
            //        remarkinfo.Add(wsheet.Name, wsheet.get_Range("B1").Value);
            //        wsheet.get_Range("1:1").Delete();
            //        filepath = System.IO.Path.Combine(path, wsheet.Name);
            //        wbook.SaveAs(filepath, Excel.XlFileFormat.xlCSV);
            //        filepath = System.IO.Path.Combine(path, wsheet.Name);
            //        iniContent.AppendFormat("[{0}]" + Environment.NewLine, wsheet.Name + ".csv");
            //        iniContent.AppendLine("Col1=seq_id Text");
            //        iniContent.AppendLine("Col2=lot_id Text");
            //        iniContent.AppendLine("Col3=item Text");
            //        iniContent.AppendLine("Col4=item_flag Text");
            //        iniContent.AppendLine("Col5=unit Text");
            //        iniContent.AppendLine("Col6=item_value Text");
            //    }
            //}
            ////為了可以讀取正確的 csv 到 DataTable 需要先設定 ini 檔
            //string iniPath = System.IO.Path.Combine(path, "Schema.ini");
            //System.IO.File.WriteAllText(iniPath, iniContent.ToString());
            //xlApp.Quit();
            //xlApp = null;

            ////
            //// 開始上傳資料流程
            ////
            //foreach (KeyValuePair<string, string> item in remarkinfo)
            //{
            //    filepath = System.IO.Path.Combine(path, item.Key + ".csv");
            //    //
            //    // 讀取 csv 至 datatable
            //    //
            //    try
            //    {
            //        LCY_Database.DBTool.Fn_Upload_Exp_Result(filepath);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(null,
            //           "上傳 upload table 時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
            //           "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    //
            //    // 讀取 datatable 至 db
            //    //
            //    try
            //    {
            //        using (NpgsqlConnection conn = new NpgsqlConnection(_CONNSTRING))
            //        {
            //            conn.Open();
            //            using (NpgsqlCommand cmnd = new NpgsqlCommand())
            //            {
            //                cmnd.Connection = conn;
            //                cmnd.CommandText = "fn_insert_exp_result";
            //                cmnd.CommandType = CommandType.StoredProcedure;
            //                cmnd.ExecuteNonQuery();
            //            }
            //        }
            //        using (NpgsqlConnection conn = new NpgsqlConnection(_CONNSTRING))
            //        {
            //            conn.Open();
            //            using (NpgsqlCommand cmnd = new NpgsqlCommand())
            //            {
            //                cmnd.Connection = conn;
            //                cmnd.CommandText = "update exp_sum set remark =:r where exp_sum_index=:id";
            //                cmnd.Parameters.Add("r", NpgsqlTypes.NpgsqlDbType.Char);
            //                cmnd.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Char);
            //                cmnd.Parameters[0].Value = item.Value;
            //                cmnd.Parameters[1].Value = item.Key.Substring(0, item.Key.Length - 7);
            //                cmnd.Parameters[0].SourceColumn = "remark";
            //                cmnd.Parameters[1].SourceColumn = "exp_sum_index";
            //                cmnd.ExecuteNonQuery();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(null,
            //           "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
            //           "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    finally
            //    {
            //        MessageBox.Show(null,
            //            "上傳程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //} 
            #endregion


        }
        private void btCancel_Result_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            string fpath = e.Argument as string;
            if (fpath == null) return;


            backgroundWorker1.ReportProgress(0, "正在準備轉換 Excel 檔...");
            //
            // 將 Excel 內的 sheet 轉存成 csv 並取出 remark
            // 
            Excel.Application xlApp = new Excel.Application();
            xlApp.Visible = false;
            xlApp.DisplayAlerts = false;
            xlApp.Workbooks.Open(fpath);
            string path = Environment.GetEnvironmentVariable("tmp");
            path = System.IO.Path.Combine(path, "Minitab", "lcy");

            //如果沒有此資料夾就新增
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            string filepath;
            Excel.Workbook wbook = xlApp.ActiveWorkbook;
            Dictionary<string, string> remarkinfo = new Dictionary<string, string>();

            backgroundWorker1.ReportProgress(0, "計算工作進度...");
            int maxprogress = wbook.Worksheets.Count * 2 + 1;
            int progress = 0;
            backgroundWorker1.ReportProgress((progress += 1) * 100 / maxprogress, "準備轉存至 csv 檔");
            //
            // 將 Excel 拆解成多份 csv 檔
            //
            StringBuilder iniContent = new StringBuilder();
            string xx;
            foreach (Excel._Worksheet wsheet in wbook.Worksheets)
            {
                if (wsheet.Name.EndsWith("_Upload"))
                {
                    wsheet.Activate();
                    //xx = wsheet.get_Range("B1").Value == null ? "aa" : wsheet.get_Range("B1").Value.ToString();
                    //remarkinfo.Add(wsheet.Name, wsheet.get_Range("B1").Value.ToString());
                    remarkinfo.Add(wsheet.Name, wsheet.get_Range("B1").Value == null ? "" : wsheet.get_Range("B1").Value.ToString());
                    backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "擷取 remark 資訊完成");
                    wsheet.get_Range("1:1").Delete();
                    backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "刪除 remark row 完成");
                    filepath = System.IO.Path.Combine(path, wsheet.Name);

                    wbook.SaveAs(filepath, Excel.XlFileFormat.xlCSV);
                    backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "儲存 csv 完成");
                    filepath = System.IO.Path.Combine(path, wsheet.Name);
                    iniContent.AppendFormat("[{0}]" + Environment.NewLine, wsheet.Name + ".csv");
                    iniContent.AppendLine("Col1=seq_id Text");
                    iniContent.AppendLine("Col2=lot_id Text");
                    iniContent.AppendLine("Col3=item Text");
                    iniContent.AppendLine("Col4=item_flag Text");
                    iniContent.AppendLine("Col5=unit Text");
                    iniContent.AppendLine("Col6=item_value Text");
                    backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "紀錄 ini 檔完成");
                }
                backgroundWorker1.ReportProgress((progress += 1) * 100 / maxprogress, string.Format("工作表{0} 處理完成", wsheet.Name));
            }
            //這裡修正工作進度最大值
            maxprogress = wbook.Worksheets.Count + remarkinfo.Count + 1;

            //為了可以讀取正確的 csv 到 DataTable 需要先設定 ini 檔
            string iniPath = System.IO.Path.Combine(path, "Schema.ini");
            System.IO.File.WriteAllText(iniPath, iniContent.ToString());
            xlApp.Quit();
            xlApp = null;

            //
            // 檢查 csv 內容是否合法: 每張上傳表 lot 需唯一且不為空，項目數需一致
            //
            backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "正在檢查上傳表內容...");
            try
            {
                foreach (KeyValuePair<string, string> item in remarkinfo)
                {
                    filepath = System.IO.Path.Combine(path, item.Key + ".csv");
                    //
                    // 讀取 csv 至 datatable
                    //

                    DataTable dt = LCY_DBTools.DBTool.ReadCSVFile(filepath);
                    DataRow[] dr = dt.Select("lot_id is null");
                    if (dr.Length > 0)
                    {                        
                        throw new Exception(string.Format("{0} 上傳的 lot_id 有空值，lot_id 不可為空", item.Key));                        
                    }

                    dr = dt.Select("lot_id is not null");
                    if (dr.Length > 0 && dr.Select(x => x["lot_id"]).Distinct().Count() > 1)
                    {
                        throw new Exception(string.Format("{0} 上傳內容包含兩個以上的 lot_id\r\n同一個上傳表內的 lot_id 需唯一。", item.Key));
                    }
                    //dr = dt.Rows.Cast<DataRow>().Where(x=> x["lot_id"] != DBNull).Select(x=>)

                }

            }
            catch (Exception ex)
            {
                
                throw new Exception("上傳 upload table 時發生錯誤:\r\n" + ex.Message);
                //MessageBox.Show(null,
                //          "上傳 upload table 時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                //          "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }


            //
            // 開始上傳資料流程
            //
            backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, "開始上傳流程");
            foreach (KeyValuePair<string, string> item in remarkinfo)
            {
                filepath = System.IO.Path.Combine(path, item.Key + ".csv");
                //
                // 讀取 csv 至 datatable
                //
                backgroundWorker1.ReportProgress((progress) * 100 / maxprogress, string.Format("正在處理上傳單#{0}", item.Key));
                try
                {
                    LCY_DBTools.DBTool.Fn_Upload_Exp_Result(filepath);
                }
                catch (Exception ex)
                {
                    throw new Exception("上傳 upload table 時發生錯誤:\r\n" + ex.Message + "\r\n 請洽管理者");
                    //MessageBox.Show(null,
                    //   "上傳 upload table 時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                    //   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }

                //
                // 讀取 datatable 至 db
                //
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        using (NpgsqlCommand cmnd = new NpgsqlCommand())
                        {
                            cmnd.Connection = conn;
                            cmnd.CommandText = "fn_insert_exp_result";
                            cmnd.CommandType = CommandType.StoredProcedure;
                            cmnd.ExecuteNonQuery();
                        }
                    }
                    using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
                    {
                        conn.Open();
                        using (NpgsqlCommand cmnd = new NpgsqlCommand())
                        {
                            cmnd.Connection = conn;
                            cmnd.CommandText = "update exp_sum set remark =:r where exp_sum_index=:id";
                            cmnd.Parameters.Add("r", NpgsqlTypes.NpgsqlDbType.Char);
                            cmnd.Parameters.Add("id", NpgsqlTypes.NpgsqlDbType.Char);
                            cmnd.Parameters[0].Value = item.Value ?? "";
                            cmnd.Parameters[1].Value = item.Key.Substring(0, item.Key.Length - 7);
                            cmnd.Parameters[0].SourceColumn = "remark";
                            cmnd.Parameters[1].SourceColumn = "exp_sum_index";
                            cmnd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("上傳時發生錯誤:\r\n" + ex.Message + "\r\n 請洽管理者");
                    //MessageBox.Show(null,
                    //   "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                    //   "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                backgroundWorker1.ReportProgress((progress += 1) * 100 / maxprogress, string.Format("上傳單#{0}處理完成", item.Key));
            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
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
                Form o = null;
                if (progressDialog != null) o = progressDialog;
                MessageBox.Show(o, e.Error.Message, "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
                progressDialog.Close();
            }
            else
            {
                Form o = null;
                if (progressDialog != null) o = progressDialog;

                MessageBox.Show(o, "工作完成", "",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                progressDialog.Close();


            }
            //this.Dispose();
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BgWorkerArgs bkArgs = e.Argument as BgWorkerArgs;
            string[] seqIdArray = bkArgs.SeqIdArray;
            string savefilepath = bkArgs.FilePath;

            Excel.Application excel = null; //宣告 excel app
            try //對每一個序號取得上傳資料
            {
                excel = new Excel.Application(); //實體化 excel
                excel.Visible = false;
                excel.DisplayAlerts = false;
                excel.Workbooks.Add();

                int maximun = seqIdArray.Length + 1; //總進度= #seqid + 1 (儲存)
                int progress = 0;
                int percentage = 0;
                foreach (string seqId in seqIdArray)
                {
                    backgroundWorker2.ReportProgress(percentage, string.Format("準備處理{0}...", seqId));
                    DataTable dt = LCY_DBTools.DBTool.GetUploadTable(seqId); //取得上傳表資料
                    XlReportBuilder.CreateExperimentUploadTable(dt, excel);
                    progress = progress + 1;
                    percentage = (int)((double)progress / (double)maximun * 100);
                    backgroundWorker2.ReportProgress(percentage, string.Format("處理{0}完成...", seqId));
                }

                //因為會多出一個工作表1..所以刪除
                excel.ActiveWorkbook.Worksheets.get_Item(1).Delete();
                //儲存文件
                if (excel != null)
                {
                    if (savefilepath != "")
                        excel.ActiveWorkbook.SaveAs(Filename: savefilepath, FileFormat: Excel.XlFileFormat.xlWorkbookDefault);

                    progress = progress + 1;
                    percentage = (int)((double)progress / (double)maximun * 100);
                    backgroundWorker2.ReportProgress(percentage, "下載完成...");
                }
                else
                {
                    throw new Exception("儲存檔案時發生問題");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (excel != null)
                {
                    //關閉文件
                    excel.ActiveWorkbook.Close();
                    excel.Workbooks.Close();
                    excel.Quit();
                    //釋放資源
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                }
                excel = null;
            }
        }


        /**************************
         * Customer
         * 
         */
        private void btBrowse_Customer_Click(object sender, EventArgs e)
        {
            openFileDialog.Title = "開啟客戶資料表";
            openFileDialog.Filter = "CSV|*.csv";
            string path = textUploadPath_Customer.Text;
            if (path != string.Empty && System.IO.Directory.Exists(System.IO.Directory.GetParent(path).ToString()))
            {
                openFileDialog.InitialDirectory = System.IO.Directory.GetParent(path).ToString();
            }
            else
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            openFileDialog.FileName = "";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _filePath_customer = openFileDialog.FileName;
                textUploadPath_Customer.Text = _filePath_customer;
            }
            else
            {
                return;
            }
        }
        private void btOK_Customer_Click(object sender, EventArgs e)
        {
            UploadCustomerToDB();
        }
        private void btCancel_Customer_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        private void UploadCustomerToDB()
        {
            //防呆
            if (string.IsNullOrWhiteSpace(_filePath_customer))
            {
                MessageBox.Show(null, "請指定檔案路徑", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!System.IO.File.Exists(_filePath_customer))
            {
                MessageBox.Show(null, "檔案不存在，請確認路徑是否正確。", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //上傳資料至 upload table
            try
            {
                LCY_DBTools.DBTool.Fn_Upload_Customer(_filePath_customer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(null,
                       "上傳 upload table 時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                       "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //執行 fn (將 upload table 的內容轉到 customer )
            using (NpgsqlConnection conn = new NpgsqlConnection(_connString))
            {
                conn.Open();
                int count_item = 0;
                try
                {
                    NpgsqlCommand cmnd = new NpgsqlCommand();
                    cmnd.Connection = conn;
                    cmnd.CommandType = CommandType.StoredProcedure;
                    cmnd.CommandText = "fn_insert_customer";
                    count_item = cmnd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(null,
                           "上傳時發生錯誤:" + ex.Message + "\r\n 請洽管理者",
                           "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    MessageBox.Show(null,
                        "上傳程序結束", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
        }


        /**************************
         * General
         * 
         */
        private void FrmUpload_Load(object sender, EventArgs e)
        {
            DataTable action;
            string[] colnames;
            /********************************
             * 
             * 機台上傳設定
             * 
             */
            action = new DataTable();
            colnames = new string[] { "action_id", "action_name" };
            foreach (string colname in colnames)
            {
                action.Columns.Add(colname);
            }
            action.Rows.Add("1", "上傳");
            action.Rows.Add("2", "編輯");
            cbPurpose_Tool.ValueMember = "action_id";
            cbPurpose_Tool.DisplayMember = "action_name";
            cbPurpose_Tool.DataSource = action;
            cbPurpose_Tool.SelectedIndex = 0;

            /********************************
             * 
             * 物性/配方上傳設定
             * 
             */
            action = new DataTable();
            colnames = new string[] { "action_id", "action_name" };
            foreach (string colname in colnames)
            {
                action.Columns.Add(colname);
            }
            action.Rows.Add("1", "上傳");
            action.Rows.Add("2", "編輯");
            cbPurpose_Item.ValueMember = "action_id";
            cbPurpose_Item.DisplayMember = "action_name";
            cbPurpose_Item.DataSource = action;
            cbPurpose_Item.SelectedIndex = 0;

            /********************************
             * 
             * 測試計畫 Tab 設定
             * 
             */
            #region 測試計畫控制項設定
            DataTable tool_type = new DataTable();
            colnames = new string[] { "type_id", "type_name" };
            foreach (string colname in colnames)
            {
                tool_type.Columns.Add(colname);
            }
            tool_type.Rows.Add("C", "混練機台");
            tool_type.Rows.Add("I", "射出機台");
            cbToolType_Plan.ValueMember = "type_id";
            cbToolType_Plan.DisplayMember = "type_name";
            cbToolType_Plan.DataSource = tool_type;

            //
            // 設定編輯類型
            //
            action = new DataTable();
            colnames = new string[] { "action_id", "action_name" };
            foreach (string colname in colnames)
            {
                action.Columns.Add(colname);
            }
            action.Rows.Add("1", "新增");
            action.Rows.Add("2", "編輯");
            action.Rows.Add("3", "刪除");
            cbTestPlanEdit.ValueMember = "action_id";
            cbTestPlanEdit.DisplayMember = "action_name";
            cbTestPlanEdit.DataSource = action;
            //
            // 元件顯示處理
            //
            textTestPlanName.Visible = true;
            textTestPlanName.Location = new Point(365, 53);
            cbSelTestPlan.Visible = false;

            //加入事件，否則會過早觸發
            this.cbTestPlanEdit.SelectedIndexChanged += cbTestPlanEdit_SelectedIndexChanged;
            this.cbSelTestPlan.SelectedIndexChanged += cbSelTestPlan_SelectedIndexChanged;
            #endregion




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
        private void RefreshDialogByLogonStatus(UserType userType)
        {
            switch (userType)
            {
                case UserType.ADMIN:
                    foreach (Control control in tabPagTool.Controls)
                    {
                        control.Enabled = true;
                    }
                    foreach (Control control in tabPagePlan.Controls)
                    {
                        control.Enabled = true;
                    }
                    foreach (Control control in tabPageExp.Controls)
                    {
                        control.Enabled = true;
                    }
                    foreach (Control control in tabPageItems.Controls)
                    {
                        control.Enabled = true;
                    }
                    foreach (Control control in tabPageCustomer.Controls)
                    {
                        control.Enabled = true;
                    }
                    break;
                case UserType.USER:
                    foreach (Control control in tabPagTool.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPagePlan.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPageExp.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPageCustomer.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPageItems.Controls)
                    {
                        control.Enabled = true;
                    }
                    //
                    // 針對 Item tabpage
                    //
                    rbtTItem.Enabled = false;
                    rbtRItem.Checked = true;
                    break;
                default:
                    foreach (Control control in tabPagTool.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPagePlan.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPageExp.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPageItems.Controls)
                    {
                        control.Enabled = false;
                    }
                    foreach (Control control in tabPageCustomer.Controls)
                    {
                        control.Enabled = false;
                    }
                    break;
            }
        }

        #endregion


    }
}
