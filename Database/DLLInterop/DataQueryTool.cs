using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCY_Database.DLLInterop
{
    [ClassInterface(ClassInterfaceType.None)] //自己設計接口
    public class DataQueryTool : IDataQuery
    {
        public DataQueryTool()
        {
            Application.EnableVisualStyles();
        }
        public void ShowDialog(Mtb.Project proj)
        {
            using (FrmQuery f = new FrmQuery(proj))
            {
                if (LCY_DBTools.DBTool.TestConnString(LCY_DBTools.DBTool.GetConnString()))
                {
                    f.ShowDialog();
                }
            }

        }

        public void Dispose()
        {

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
