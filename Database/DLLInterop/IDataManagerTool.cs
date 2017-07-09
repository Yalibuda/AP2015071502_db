using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database.DLLInterop
{
    public interface IDataManagerTool
    {
        UserType UserType { set; get; }
        string UserName { set; get; }
        void ShowDialog();        
        void Dispose();
    }
}
