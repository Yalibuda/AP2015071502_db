using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database.Tools
{
    public static class ItemFunction
    {
        /// <summary>
        /// 判斷 list1 中是否有包含某個Item, 比較的準則是使用 Name + Unit 去看，適用於 Formulation
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="item"></param>
        /// <returns>true or false</returns>
        public static bool CompareFormulationCollection(List<IItem> list1, IItem item)
        {
            if (list1.Select((x, i) =>
                               object.Equals(x.Name + x.Unit, item.Name + item.Unit) ? i : -1).Where(x => x != -1).FirstOrDefault() == 0 &&
                list1[0].Name + list1[0].Unit != item.Name + item.Unit)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判斷 list1 中是否有包含某個Item, 比較的準則是使用 Name + Unit + Type 去看，適用於 Property
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="item"></param>
        /// <returns>true or false</returns>
        public static bool ComparePropertyCollection(List<IItem> list1, IItem item)
        {
            if (list1.Select((x, i) =>
                               object.Equals(x.Name + x.Unit + x.ProdType, item.Name + item.Unit + item.ProdType) ? i : -1).Where(x => x != -1).FirstOrDefault() == 0 &&
                list1[0].Name + list1[0].Unit + list1[0].ProdType != item.Name + item.Unit + item.ProdType)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
