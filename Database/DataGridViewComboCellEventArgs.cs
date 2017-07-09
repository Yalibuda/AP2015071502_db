using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    class DataGridViewComboCellEventArgs:EventArgs
    {
        public int ColumnIndex { set; get; }
        public int RowIndex { set; get; }
        public DataGridViewComboCellEventArgs(int rowID, int colID):base()
        {
            ColumnIndex = colID;
            RowIndex = rowID;
        }
    }
}
