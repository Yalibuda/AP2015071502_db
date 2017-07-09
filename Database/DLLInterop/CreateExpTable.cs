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
    public class CreateExpTable : IDataManagerTool
    {
        public CreateExpTable()
        {
            Application.EnableVisualStyles();
            UserType = LCY_Database.UserType.USER;
            //System.Windows.Forms.Application.EnableVisualStyles();
        }
        public UserType UserType { set; get; }
        public string UserName { set; get; }

        public void ShowDialog()
        {
            using (FrmCreateTable f = new FrmCreateTable(this.UserType))
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
            GC.WaitForPendingFinalizers(); throw new NotImplementedException();
        }
    }
}
