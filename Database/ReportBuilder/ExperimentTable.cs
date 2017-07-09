using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database.ReportBuilder
{
    public class ExperimentTable : IDisposable
    {
        public ExperimentTable()
        {

        }
        public string SeqID { set; get; }
        public string LotID { set; get; }
        public ITool CompTool { set; get; }
        public ITool InjTool { set; get; }
        public KeyValuePair<string, string> User { set; get; }
        public KeyValuePair<string, string> Customer { set; get; }
        public string Purpose { set; get; }    
        public string ApplyDate { set; get; }
        public string RunDate { set; get; }
        public string Grade { set; get; }
        public string ProdType { set; get; }
        public double BagNumber { set; get; }
        public double BagWeight { set; get; }
        public string Remark { set; get; }
        public List<IItem> Formulas { set; get; }
        public List<IItem> Property { set; get; }

        public void Run()
        {

        }

        public override string ToString()
        {
            StringBuilder info = new StringBuilder();
            info.AppendFormat("User: {0}\r\n", User.Value);
            info.AppendFormat("Purpose: {0}\r\n", Purpose);
            info.AppendFormat("Apply date: {0}\r\n", ApplyDate);
            info.AppendFormat("Run date: {0}\r\n", RunDate);
            info.AppendFormat("Customer: {0}\r\n", Customer.Value);
            info.AppendFormat("#Bag: {0}, Weight of each Bag: {1}\r\n", BagNumber, BagWeight);
            info.AppendFormat("Product: {0}. Grade: {1}\r\n", ProdType, Grade);
            return info.ToString();
        }

        public DataTable GetExperimentInitialInfo()
        {
            //"exp_date","c_tool_id","i_tool_id","purpose","customer_id","grade","type","emp_id","remark","item_id","item_value","item_flag","item_name","unit"
            DataTable exp_int = new DataTable();
            string[] col_exp_int = new string[] { "exp_date", "c_tool_id", "i_tool_id", 
                "purpose", "customer_id", "grade", "type", "emp_id", "remark", "item_id", 
                "item_value", "item_flag", "item_name", "unit" };
            foreach (string col in col_exp_int)
            {
                exp_int.Columns.Add(col);
            }

            List<IItem> _ttlItems = new List<IItem>();
            if (CompTool.Items != null) _ttlItems = _ttlItems.Union(CompTool.Items).ToList();
            if (InjTool.Items != null) _ttlItems = _ttlItems.Union(InjTool.Items).ToList();
            if (Formulas != null) _ttlItems = _ttlItems.Union(Formulas).ToList();
            if (Property != null) _ttlItems = _ttlItems.Union(Property).ToList();

            //List<IItem> _ttlItems = CompTool.Items.Union(InjTool.Items.Union(Formulas.Union(Property))).ToList();

            foreach (IItem item in _ttlItems)
            {
                DataRow dr = exp_int.NewRow();
                dr["exp_date"] = RunDate;
                dr["c_tool_id"] = CompTool.ID;
                dr["i_tool_id"] = InjTool.ID;
                dr["purpose"] = Purpose;
                dr["customer_id"] = Customer.Key;
                dr["grade"] = Grade;
                dr["type"] = ProdType;
                dr["emp_id"] = User.Key;
                dr["remark"] = Remark;
                dr["item_id"] = item.ID;
                dr["item_value"] = item.Value;
                dr["item_flag"] = item.Flag;
                dr["item_name"] = item.Name;
                dr["unit"] = item.Unit;
                exp_int.Rows.Add(dr);
            }


            return exp_int;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
