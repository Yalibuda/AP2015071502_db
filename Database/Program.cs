using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace LCY_Database
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if DEBUG
            //ReportBuilder.ExperimentTable exp = new ReportBuilder.ExperimentTable();
            //using (NpgsqlConnection conn = new NpgsqlConnection(Properties.Settings.Default.Connectstring))
            //{
            //    conn.Open();
            //    NpgsqlDataAdapter da;
            //    DataSet ds;
            //    DataTable dt;
            //    NpgsqlCommand cmnd = new NpgsqlCommand();
            //    List<IItem> items;
            //    cmnd.Connection = conn;

            //    //insert r item
            //    cmnd.CommandText = "select item_id, item_name, item_flag, type, unit from item where item_flag like 'R%' limit 3";
            //    da = new NpgsqlDataAdapter(cmnd);
            //    ds = new DataSet();
            //    da.Fill(ds);
            //    dt = ds.Tables[0];
            //    items = new List<IItem>();
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        IItem a = new Item();
            //        a.ID = r["item_id"].ToString();
            //        a.Name = r["item_name"].ToString();
            //        a.ProdType = r["type"].ToString();
            //        a.Unit = r["unit"].ToString();
            //        a.Value = new Random().NextDouble().ToString();
            //        items.Add(a);
            //    }
            //    exp.Formulas = items;

            //    //insert p item
            //    cmnd.CommandText = "select item_id, item_name, item_flag, type, unit from item where item_flag like 'P%' limit 5";
            //    da = new NpgsqlDataAdapter(cmnd);
            //    ds = new DataSet();
            //    da.Fill(ds);
            //    dt = ds.Tables[0];
            //    items = new List<IItem>();
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        IItem a = new Item();
            //        a.ID = r["item_id"].ToString();
            //        a.Name = r["item_name"].ToString();
            //        a.ProdType = r["type"].ToString();
            //        a.Unit = r["unit"].ToString();
            //        a.Value = new Random().NextDouble().ToString();
            //        items.Add(a);
            //    }
            //    exp.Property = items;

            //    //insert c tool
            //    cmnd.CommandText = "select t.tool_id, t.tool_name, t.tool_type, tp.item_id, i.item_name, i.item_flag, tp.item_value, i.unit, i.type  " +
            //        "from tool_sum t, tool_item_info ti, item i, test_plan tp " +
            //        "where ti.tool_id = t.tool_id and ti.item_id= tp.item_id and tp.item_id=i.item_id and " +
            //        "t.tool_id='PSM30A' and tp.test_plan_id in ('Plan1','TPlan1') order by item_flag, i.item_name";
            //    da = new NpgsqlDataAdapter(cmnd);
            //    ds = new DataSet();
            //    da.Fill(ds);
            //    dt = ds.Tables[0];
            //    ITool tool = new Tool();
            //    tool.Flag = dt.Rows[0]["tool_type"].ToString();
            //    tool.ID = dt.Rows[0]["tool_id"].ToString();
            //    tool.Name = dt.Rows[0]["tool_name"].ToString();

            //    //set test plan
            //    items = new List<IItem>();
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        IItem a = new Item();
            //        a.ID = r["item_id"].ToString();
            //        a.Name = r["item_name"].ToString();
            //        a.Flag = r["item_flag"].ToString();
            //        a.ProdType = r["type"].ToString();
            //        a.Unit = r["unit"].ToString();
            //        a.Value = r["item_value"].ToString();
            //        items.Add(a);
            //    }
            //    tool.Items = items.ToArray();
            //    exp.CompTool = tool;

            //    //insert i tool
            //    cmnd.CommandText = "select t.tool_id, t.tool_name, t.tool_type, tp.item_id, i.item_name, i.item_flag, tp.item_value, i.unit,i.type  " +
            //        "from tool_sum t, tool_item_info ti, item i, test_plan tp " +
            //        "where ti.tool_id = t.tool_id and ti.item_id= tp.item_id and tp.item_id=i.item_id and " +
            //        "t.tool_id='INJ33' and tp.test_plan_id in ('Plan1','TPlan1') order by item_flag, i.item_name";
            //    da = new NpgsqlDataAdapter(cmnd);
            //    ds = new DataSet();
            //    da.Fill(ds);
            //    dt = ds.Tables[0];
            //    tool = new Tool();
            //    tool.Flag = dt.Rows[0]["tool_type"].ToString();
            //    tool.ID = dt.Rows[0]["tool_id"].ToString();
            //    tool.Name = dt.Rows[0]["tool_name"].ToString();
            //    //set test plan
            //    items = new List<IItem>();
            //    foreach (DataRow r in dt.Rows)
            //    {
            //        IItem a = new Item();
            //        a.ID = r["item_id"].ToString();
            //        a.Name = r["item_name"].ToString();
            //        a.Flag = r["item_flag"].ToString();
            //        a.ProdType = r["type"].ToString();
            //        a.Unit = r["unit"].ToString();
            //        a.Value = r["item_value"].ToString();
            //        items.Add(a);
            //    }
            //    tool.Items = items.ToArray();
            //    exp.InjTool = tool;

            //    //set basic info
            //    exp.Purpose = "OOXX";
            //    exp.ApplyDate = "2015/11/12";
            //    exp.RunDate = "2015/11/30";
            //    exp.Customer = new KeyValuePair<string, string>("22435367", "SFI");
            //    exp.User = new KeyValuePair<string, string>("53101", "Mabo Lee");
            //    exp.BagNumber = 5;
            //    exp.BagWeight = 20;
            //    exp.Grade = "testgrade";
            //    exp.ProdType = "PCMA";

            //    //MessageBox.Show(exp.ToString());
            //    Excel.Application excel = new Excel.Application();
            //    excel.Workbooks.Add();
            //    excel.Visible = true;
            //    excel.DisplayAlerts = false;
            //    try
            //    {
            //        ReportBuilder.XlReportBuilder.CreateExperimentTable(exp, excel);
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }
            //    finally
            //    {
            //        excel.Quit();
            //    }
                
                               
           // }


#endif
            //Application.Run(new FrmUpload( UserType.ADMIN));
            //Application.Run(new FrmLogon());
            //Application.Run(new FrmCreateTable());
            
            //Mtb.Application mtbApp = new Mtb.Application();
            //mtbApp.UserInterface.Visible = true;
            //mtbApp.UserInterface.DisplayAlerts = false;
            //Mtb.Project proj = mtbApp.ActiveProject;
            Application.Run(new FrmQuery());
            //Application.Run(new FrmOption());
            //Application.Run(new FrmSelectSeq(null));
        }
    }
}
