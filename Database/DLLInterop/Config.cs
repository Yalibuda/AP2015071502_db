using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database.DLLInterop
{
    [ClassInterface(ClassInterfaceType.None)] //自己設計接口
    public class Config:IMyApp
    {
        public Config()
        {
            System.Windows.Forms.Application.EnableVisualStyles();    
        }
        public void ShowDialog()
        {
            using (System.Windows.Forms.Form f = new FrmOption())
            {
                f.ShowDialog();
            }
        }

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
