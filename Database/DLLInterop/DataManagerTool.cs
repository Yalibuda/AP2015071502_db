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
    public class DataManagerTool : IDataManagerTool
    {
        public DataManagerTool()
        {
            Application.EnableVisualStyles();
            UserType = LCY_Database.UserType.NONE;

        }
        public UserType UserType { set; get; }
        public string UserName { set; get; }
        public void ShowDialog()
        {
            if (!LCY_DBTools.DBTool.TestConnString(LCY_DBTools.DBTool.GetConnString()))
            {
                return;
            }

            if (UserType == LCY_Database.UserType.NONE)
            {
                using (FrmLogon f = new FrmLogon())
                {
                    if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        UserType = f.GetUserType();
                    }
                    else
                    {
                        return;
                    }

                }
            }
            using (FrmUpload f = new FrmUpload(UserType))
            {
                f.ShowDialog();
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
