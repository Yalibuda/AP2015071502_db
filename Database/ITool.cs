using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    public interface ITool
    {
        string ID { set; get; }
        string Name { set; get; }
        string Flag { set; get; }
        IItem[] Items { set; get; }
    }
}
