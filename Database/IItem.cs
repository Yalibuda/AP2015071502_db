using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    public interface IItem
    {
        string ID { set; get; }
        string Name { set; get; }
        string Value { set; get; }
        string Flag { set; get; }
        string Unit { set; get; }
        string ProdType { set; get; }
        int Item_Seq { set; get; }
        object Tag { set; get; }
        IItem DeepClon();
    }
}
