using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database.DLLInterop
{
    public interface IDataQuery
    {
        void ShowDialog(Mtb.Project proj);
        void Dispose();
    }
}
