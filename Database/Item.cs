using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    public class Item:IItem
    {
        public Item()
        {
            Item_Seq = -1;
            Tag = null;
        }
        public string ID { set; get; }
        public string Name { set; get; }
        public string Value { set; get; }
        public string Flag { set; get; }
        public string ProdType { set; get; }
        public string Unit { set; get; }
        public int Item_Seq { set; get; }
        public object Tag { set; get; }

        public IItem DeepClon()
        {
            return (IItem)this.MemberwiseClone();
        }
    }
}
