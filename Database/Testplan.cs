using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    public class Testplan
    {
        public Testplan()
        {
            Name = "";
            Items = null;
        }

        public string Name { set; get; }
        public IItem[] Items { set; get; }
        public override string ToString()
        {
            return Name;
        }

        
    }
}
