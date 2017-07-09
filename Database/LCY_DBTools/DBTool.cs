using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Npgsql;
using NpgsqlTypes;
using System.Data.OleDb;
using System.Windows.Forms;



namespace LCY_Database.LCY_DBTools
{
    public static class DBTool
    {
        /// <summary>
        /// 上傳指定的 CSV 檔案至資料庫的表格 upload_exp_init
        /// </summary>
        /// <param name="filepath"></param>
        public static void Fn_Upload_Exp_Init(string filepath)
        {
            DataTable dt = ReadCSVFile(filepath);
            Fn_Upload_Exp_Init(dt);
        }
        /// <summary>
        /// Upload the specific datatable to table upload_exp_init
        /// </summary>
        /// <param name="_dt">datatable include field c.t. upload_exp_init</param>
        public static void Fn_Upload_Exp_Init(DataTable _dt)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from upload_exp_init", conn);
                    da.DeleteCommand = new NpgsqlCommand("delete from upload_exp_init", conn);
                    da.InsertCommand = new NpgsqlCommand("insert into upload_exp_init values (:c1,:c2,:c3,:c4,:c5,:c6,:c7,:c8,:c9,:c10,:c11,:c12,:c13,:c14)", conn);
                    string[] paraArray = new string[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14" };
                    foreach (var para in paraArray)
                    {
                        da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Char);
                    }
                    string[] colnames = new string[] { "exp_date", "c_tool_id", "i_tool_id", "purpose", "customer_id", "grade", "type", "emp_id", "remark", "item_id", "item_value", "item_flag", "item_name", "unit" };
                    for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                    {
                        da.InsertCommand.Parameters[i].Direction = ParameterDirection.Input;
                        da.InsertCommand.Parameters[i].SourceColumn = colnames[i];
                    }
                    //Delete 
                    da.DeleteCommand.ExecuteNonQuery();
                    da.Fill(ds);

                    //Insert
                    DataTable dt = ds.Tables[0];
                    dt.Merge(_dt);
                    DataSet d2 = ds.GetChanges();
                    if (d2 != null)
                    {
                        da.Update(d2);
                        ds.Merge(d2);
                    }
                    ds.AcceptChanges();

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        public static void Fn_Upload_Exp_Result(string filepath)
        {
            DataTable dt = ReadCSVFile(filepath);
            Fn_Upload_Exp_Result(dt);
        }
        public static void Fn_Upload_Exp_Result(DataTable _dt)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from upload_exp_result", conn);
                    da.DeleteCommand = new NpgsqlCommand("delete from upload_exp_result", conn);
                    da.InsertCommand = new NpgsqlCommand("insert into upload_exp_result values (:c1,:c2,:c3,:c4,:c5,:c6)", conn);
                    string[] paraArray = new string[] { "c1", "c2", "c3", "c4", "c5", "c6" };
                    foreach (var para in paraArray)
                    {
                        da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Char);
                    }
                    string[] colnames = new string[] { "seq_id", "lot_id", "item", "item_flag", "unit", "item_value" };
                    for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                    {
                        da.InsertCommand.Parameters[i].Direction = ParameterDirection.Input;
                        da.InsertCommand.Parameters[i].SourceColumn = colnames[i];
                    }
                    //Delete 
                    da.DeleteCommand.ExecuteNonQuery();
                    da.Fill(ds);

                    //Insert
                    //DataTable dt = ds.Tables[0];
                    //dt.Merge(_dt);
                    //Insert(不知為何使用 merge 會無法取得 update() 的物件...)
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in _dt.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["seq_id"] = dr["seq_id"].ToString();
                        row["lot_id"] = dr["lot_id"];
                        row["item"] = dr["item"];
                        row["item_flag"] = dr["item_flag"];
                        row["unit"] = dr["unit"];
                        row["item_value"] = dr["item_value"];
                        dt.Rows.Add(row);
                    }
                    DataSet d2 = ds.GetChanges();
                    if (d2 != null)
                    {
                        da.Update(d2);
                        ds.Merge(d2);
                    }
                    ds.AcceptChanges();

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        /// <summary>
        /// 上傳指定的 CSV 檔案至資料庫的表格 upload_test_plan
        /// </summary>
        /// <param name="filepath"></param>
        public static void Fn_Upload_Test_Plan(string filepath)
        {
            DataTable dt = ReadCSVFile(filepath);
            if (VerifyUploadTestPlan(dt))
            {
                Fn_Upload_Test_Plan(dt); //判斷 test plan 資料格式正確才上傳
            }


        }

        private static bool VerifyUploadTestPlan(DataTable _dt)
        {
            //
            // 判斷有無 null row..
            //
            DataRow[] toolbase = _dt.Select("value is null");
            if (toolbase == null || toolbase.Count() == 0) //表示沒有機台結構列
            {
                throw new Exception("無機台基本資訊列");
            }

            // 機台 + item + item_flag + unit 應該要唯一，其數量應該要等於 null 總數            
            string[] uniqItems = toolbase.Select(x => x["tool_name"].ToString() +
                x["item_name"].ToString() +
                x["item_flag"].ToString() +
                x["unit"].ToString()).Distinct().ToArray();
            if (uniqItems.Length < toolbase.Count())
            {
                throw new Exception("機台基本資訊列有重複");
            }

            // item_seq_id 也不能有重複 ==> 會無法辨識順序
            string[] uniqSeq = toolbase.Select(x => x["item_seq_id"].ToString()).Distinct().ToArray();
            if (uniqSeq.Length < toolbase.Count())
            {
                throw new Exception("機台基本資訊列的順序值有重複");
            }

            //組出辨識用的機台資訊列
            uniqItems = toolbase.Select(x => x["item_seq_id"].ToString() +
                x["tool_name"].ToString() +
                x["item_name"].ToString() +
                x["item_flag"].ToString() +
                x["unit"].ToString()
                ).Distinct().ToArray();

            //算出溫度條件和加工條件
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[CI]T");
            string[] procItem = null;
            try
            {
                procItem = toolbase.Where(x => !regex.IsMatch(x["item_flag"].ToString())).Select(x => x["item_seq_id"].ToString() +
                x["tool_name"].ToString() +
                x["item_name"].ToString() +
                x["item_flag"].ToString() +
                x["unit"].ToString()
                ).Distinct().ToArray();
            }
            catch (Exception)
            {
                //表示機台資訊無區段溫度設定                
            }


            string[] tempItem = null;
            try
            {
                tempItem = toolbase.Where(x => regex.IsMatch(x["item_flag"].ToString())).Select(x => x["item_seq_id"].ToString() +
                   x["tool_name"].ToString() +
                   x["item_name"].ToString() +
                   x["item_flag"].ToString() +
                   x["unit"].ToString()
                   ).Distinct().ToArray();
            }
            catch (Exception)
            {
                // 表示機台資訊無加工設定                
            }


            //判斷是否有 testplan 要上傳
            DataRow[] testplans = _dt.Select("value is not null");
            string[] testPlanNameCollection = testplans.Select(x => x["test_plan"].ToString()).Distinct().ToArray();

            foreach (string planName in testPlanNameCollection)
            {
                DataRow[] testPlanItemCollection = testplans.Where(x => x["test_plan"].ToString() == planName).ToArray();
                //建立辨識項
                string[] uniq = testPlanItemCollection.Select(
                    x => x["item_seq_id"].ToString() +
                        x["tool_name"].ToString() +
                        x["item_name"].ToString() +
                        x["item_flag"].ToString() +
                        x["unit"].ToString()).Distinct().ToArray();
                string planType = "";
                if (regex.IsMatch(testPlanItemCollection[0]["item_flag"].ToString()))//判斷測試計畫類型
                {
                    planType = "temp"; //區段溫度
                }
                else
                {
                    planType = "proc"; //加工條件
                }

                switch (planType)
                {
                    case "temp":
                        if (tempItem == null)
                        {
                            throw new Exception(string.Format("測試計畫 {0} 中有溫度條件，但是機台未有溫度條件", planName));
                        }
                        //比對總數量或 distinct 後的數量(可能少填入某項重複某項剛好總數相同)是否相同
                        if (testPlanItemCollection.Count() != tempItem.Length || uniq.Length != tempItem.Length)
                        {
                            throw new Exception(string.Format("測試計畫 {0} 的條件數量與機台定義不符", planName));
                        }

                        //比對測試計畫中是否包含機台未包含之項目
                        foreach (var item in uniq)
                        {
                            if (!tempItem.Contains(item))
                            {
                                throw new Exception(string.Format("測試計畫 {0} 中包含機台未定義項目", planName));
                            }
                        }
                        break;
                    case "proc":
                        if (procItem == null)
                        {
                            throw new Exception(string.Format("測試計畫 {0} 中有加工條件，但是機台未有加工條件", planName));
                        }
                        //比對總數量或 distinct 後的數量(可能少填入某項重複某項剛好總數相同)是否相同
                        if (testPlanItemCollection.Count() != procItem.Length || uniq.Length != procItem.Length)
                        {
                            throw new Exception(string.Format("測試計畫{0}的條件數量與機台定義不符", planName));
                        }

                        //比對測試計畫中是否包含機台未包含之項目
                        foreach (var item in uniq)
                        {
                            if (!procItem.Contains(item))
                            {
                                throw new Exception(string.Format("測試計畫{0}中包含機台未定義項目", planName));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            return true;
        }

        /// <summary>
        /// Upload the specific datatable to table upload_exp_init
        /// </summary>
        /// <param name="_dt">datatable include field c.t. upload_test_plan</param>
        public static void Fn_Upload_Test_Plan(DataTable _dt)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from upload_test_plan", conn);
                    da.DeleteCommand = new NpgsqlCommand("delete from upload_test_plan", conn);
                    da.InsertCommand = new NpgsqlCommand("insert into upload_test_plan values (:c1,:c2,:c3,:c4,:c5,:c6,:c7, :c8)", conn);

                    string[] paraArray = new string[] { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8" };
                    foreach (var para in paraArray)
                    {
                        if (para == "c8")
                        {
                            da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Integer);
                        }
                        else
                        {
                            da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Char);
                        }


                    }
                    string[] colnames = new string[] { "tool_name", "tool_type", "item_name", "item_flag", "unit", "value", "test_plan", "item_seq_id" };
                    for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                    {
                        da.InsertCommand.Parameters[i].Direction = ParameterDirection.InputOutput;
                        da.InsertCommand.Parameters[i].SourceColumn = colnames[i];
                    }

                    //Delete 
                    da.DeleteCommand.ExecuteNonQuery();
                    da.Fill(ds);

                    //Insert(不知為何使用 merge 會無法取得 update() 的物件...)
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in _dt.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["tool_name"] = dr["tool_name"];
                        row["tool_type"] = dr["tool_type"];
                        row["item_name"] = dr["item_name"];
                        row["item_flag"] = dr["item_flag"];
                        row["unit"] = dr["unit"];
                        row["item_seq_id"] = dr["item_seq_id"];
                        row["value"] = dr["value"];
                        row["test_plan"] = dr["test_plan"];
                        dt.Rows.Add(row);
                    }
                    DataSet d2 = ds.GetChanges();
                    if (d2 != null)
                    {
                        da.Update(d2);
                        ds.Merge(d2);
                    }
                    ds.AcceptChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        /// <summary>
        /// 上傳指定的 CSV 檔案至資料庫的表格 upload_r_item
        /// </summary>
        /// <param name="filepath"></param>
        public static void Fn_Upload_R_Item(string filepath)
        {
            DataTable dt = ReadCSVFile(filepath);
            Fn_Upload_R_Item(dt);
        }
        /// <summary>
        /// 將 System.Data.DataTable 上傳至資料庫表格 upload_r_item
        /// </summary>
        /// <param name="_dt"></param>
        public static void Fn_Upload_R_Item(DataTable _dt)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from upload_r_item", conn);
                    da.DeleteCommand = new NpgsqlCommand("delete from upload_r_item", conn);
                    da.InsertCommand = new NpgsqlCommand("insert into upload_r_item values (:c1,:c2)", conn);
                    string[] paraArray = new string[] { "c1", "c2" };
                    foreach (var para in paraArray)
                    {
                        da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Char);
                    }
                    string[] colnames = new string[] { "r_name", "unit" };
                    for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                    {
                        da.InsertCommand.Parameters[i].Direction = ParameterDirection.Input;
                        da.InsertCommand.Parameters[i].SourceColumn = colnames[i];
                    }
                    //Delete 
                    da.DeleteCommand.ExecuteNonQuery();
                    da.Fill(ds);

                    //Insert
                    DataTable dt = ds.Tables[0];
                    //dt.Merge(_dt);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["r_name"] = dr["r_name"];
                        row["unit"] = dr["unit"];
                        dt.Rows.Add(row);
                    }

                    DataSet d2 = ds.GetChanges();
                    if (d2 != null)
                    {
                        da.Update(d2);
                        ds.Merge(d2);
                    }
                    ds.AcceptChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        /// <summary>
        /// 上傳指定的 CSV 檔案至資料庫的表格 upload_p_item
        /// </summary>
        /// <param name="filepath"></param>
        public static void Fn_Upload_P_Item(string filepath)
        {
            DataTable dt = ReadCSVFile(filepath);
            Fn_Upload_P_Item(dt);
        }
        /// <summary>
        /// 將 System.Data.DataTable 上傳至資料庫表格 upload_p_item
        /// </summary>
        /// <param name="_dt"></param>
        public static void Fn_Upload_P_Item(DataTable _dt)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from upload_p_item", conn);
                    da.DeleteCommand = new NpgsqlCommand("delete from upload_p_item", conn);
                    da.InsertCommand = new NpgsqlCommand("insert into upload_p_item values (:c1,:c2, :c3)", conn);
                    string[] paraArray = new string[] { "c1", "c2", "c3" };
                    foreach (var para in paraArray)
                    {
                        da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Char);
                    }
                    string[] colnames = new string[] { "p_name", "type", "unit" };
                    for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                    {
                        da.InsertCommand.Parameters[i].Direction = ParameterDirection.Input;
                        da.InsertCommand.Parameters[i].SourceColumn = colnames[i];
                    }
                    //Delete 
                    da.DeleteCommand.ExecuteNonQuery();
                    da.Fill(ds);

                    //Insert
                    DataTable dt = ds.Tables[0];
                    //dt.Merge(_dt);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["p_name"] = dr["p_name"];
                        row["type"] = dr["type"];
                        row["unit"] = dr["unit"];
                        dt.Rows.Add(row);
                    }
                    DataSet d2 = ds.GetChanges();
                    if (d2 != null)
                    {
                        da.Update(d2);
                        ds.Merge(d2);
                    }
                    ds.AcceptChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        /// <summary>
        /// 上傳指定的 CSV 檔案至資料庫的表格 upload_customer
        /// </summary>
        /// <param name="filepath"></param>
        public static void Fn_Upload_Customer(string filepath)
        {
            DataTable dt = ReadCSVFile(filepath);
            Fn_Upload_Customer(dt);
        }
        /// <summary>
        /// 將 System.Data.DataTable 上傳至資料庫表格 upload_customer
        /// </summary>
        /// <param name="_dt"></param>
        public static void Fn_Upload_Customer(DataTable _dt)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    DataSet ds = new DataSet();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from upload_customer", conn);
                    da.DeleteCommand = new NpgsqlCommand("delete from upload_customer", conn);
                    da.InsertCommand = new NpgsqlCommand("insert into upload_customer values (:c1,:c2, :c3)", conn);
                    string[] paraArray = new string[] { "c1", "c2", "c3" };
                    foreach (var para in paraArray)
                    {
                        da.InsertCommand.Parameters.Add(para, NpgsqlDbType.Char);
                    }
                    string[] colnames = new string[] { "id", "ename", "cname" };
                    for (int i = 0; i < da.InsertCommand.Parameters.Count; i++)
                    {
                        da.InsertCommand.Parameters[i].Direction = ParameterDirection.Input;
                        da.InsertCommand.Parameters[i].SourceColumn = colnames[i];
                    }
                    //Delete 
                    da.DeleteCommand.ExecuteNonQuery();
                    da.Fill(ds);

                    //Insert
                    DataTable dt = ds.Tables[0];
                    //dt.Merge(_dt);
                    foreach (DataRow dr in _dt.Rows)
                    {
                        DataRow row = dt.NewRow();
                        row["id"] = dr["id"];
                        row["ename"] = dr["ename"];
                        row["cname"] = dr["cname"];
                        dt.Rows.Add(row);
                    }
                    DataSet d2 = ds.GetChanges();
                    if (d2 != null)
                    {
                        da.Update(d2);
                        ds.Merge(d2);
                    }
                    ds.AcceptChanges();
                }
                catch (Exception)
                {
                    throw;
                }

            }

        }
        /// <summary>
        /// Read csv file to a System.Data.DataTable
        /// </summary>
        /// <param name="filepath">a path with file name and extension</param>
        /// <returns></returns>
        public static DataTable ReadCSVFile(string filepath)
        {
            //Check if the file & path exist?
            if (!File.Exists(filepath))
            {
                //MessageBox.Show(null, string.Format("檔案 {0} 不存在!", filepath), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new FileNotFoundException();
            }

            string fileDir = Directory.GetParent(filepath).ToString();
            string fileName = Path.GetFileName(filepath);

            string connString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=""text;HDR=Yes;FMT=Delimited;"";", fileDir);
            //string connString = "";
            //if (Environment.Is64BitOperatingSystem)
            //{
            //    connString = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=""text;HDR=Yes;FMT=Delimited;"";", fileDir);
            //}
            //else
            //{
            //    connString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties=""text;HDR=Yes;FMT=Delimited;"";", fileDir);
            //}            
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    conn.Open();
                    OleDbCommand cmnd = new OleDbCommand("SELECT * FROM " + fileName, conn);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmnd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


        }
        public static DataTable Create_Upload_Test_Plan_Table()
        {
            DataTable dt = new DataTable();
            string[] colnames = new string[] { "tool_name", "tool_type", "item_name", "item_flag", "unit", "value", "test_plan" };
            foreach (string name in colnames)
            {
                dt.Columns.Add(name);
            }
            return dt;
        }
        public static DataTable Create_Blank_Table(string[] colnames)
        {
            DataTable dt = new DataTable();
            try
            {
                foreach (string name in colnames)
                {
                    dt.Columns.Add(name);
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static bool SaveDataTableAsCSV(DataTable datatable, string path)
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
                System.IO.File.WriteAllText(path, sb.ToString(), Encoding.Default);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public static DataTable GetUploadTable(string seq_id)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
                {
                    conn.Open();
                    using (NpgsqlDataAdapter da = new NpgsqlDataAdapter())
                    {
                        da.SelectCommand = new NpgsqlCommand("select exp_sum_index as seq_id, lot_id,item_name as item,item_flag,unit,null item_value,remark " +
                            "from vw_exp_info where item_flag in ('C1','CT','I1','IT','P') and exp_sum_index=:seqid order by seq_id, left(item_flag,1), item_seq_id ", conn);
                        da.SelectCommand.Parameters.Add("seqid", NpgsqlDbType.Char);
                        da.SelectCommand.Parameters["seqid"].Value = seq_id;

                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dt.TableName = seq_id + "_Upload";
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static string GetConnString()
        {
            string _connString = string.Format("Server={0}; Port={1};User Id={2};Password={3};Database={4};Timeout=7;",
            Properties.Settings.Default.Server,
            Properties.Settings.Default.Port,
            Properties.Settings.Default.UID,
            Properties.Settings.Default.PSW,
            Properties.Settings.Default.Database);
            return _connString;
        }
        public static bool TestConnString(string connString)
        {

            using (NpgsqlConnection conn = new NpgsqlConnection(GetConnString()))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show(null,"無法連線至資料庫, 請使用\r\n資料庫工具箱> 選項\r\n確認伺服器位置是否正確, 或洽管理者。","", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
