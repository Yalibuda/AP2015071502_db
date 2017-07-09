using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCY_Database
{
    /// <summary>
    /// 此類別只用於下載上傳表資料(1個 excel file 有多個sheet 存放不同實驗序號資料)時
    /// </summary>
    public class BgWorkerArgs
    {
        public string FilePath { set; get; }
        public string[] SeqIdArray { set; get; }
    }
}
