using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LCY_Database.Tools
{
    class ListViewItemComparer : IComparer<ListViewItem>
    {
        public int Compare(ListViewItem x, ListViewItem y)
        {
            return string.Compare(x.Text, y.Text);
        }
    }
}
