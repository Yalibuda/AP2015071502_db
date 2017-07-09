using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    public class Tool:ITool
    {
        public Tool()
        {
            Items = null;
        }
        public string ID { set; get; }
        public string Name { set; get; }
        public string Flag { set; get; }
        public IItem[] Items { set; get; }
        public override string ToString()
        {
            return ID;
        }
    }
}
