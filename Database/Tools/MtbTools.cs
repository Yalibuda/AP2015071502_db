using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mtb;
using System.Collections;

namespace LCY_Database.Tools
{
    enum MtbVarType
    {
        Column,
        Constant,
        Matrix
    }
    internal static class MtbTools
    {
        internal const double MissValue = 1.23456E+30;
        internal static String[] CreateVariableStrArray(Mtb.Worksheet ws, int num, MtbVarType mType)
        {
            int cnt = 0;
            String[] varStr = new String[num]; //num have to large than 1
            try
            {
                switch (mType)
                {
                    case MtbVarType.Column:
                        cnt = ws.Columns.Count;
                        ws.Columns.Add(Quantity: num);
                        for (int i = 0; i < varStr.Length; i++)
                        {
                            varStr[i] = ws.Columns.Item(cnt + 1 + i).SynthesizedName;
                        }
                        break;
                    case MtbVarType.Constant:
                        cnt = ws.Constants.Count;
                        ws.Constants.Add(Quantity: num);
                        for (int i = 0; i < varStr.Length; i++)
                        {
                            varStr[i] = ws.Constants.Item(cnt + 1 + i).SynthesizedName;
                        }
                        break;
                    case MtbVarType.Matrix:
                        cnt = ws.Matrices.Count;
                        ws.Matrices.Add(Quantity: num);
                        for (int i = 0; i < varStr.Length; i++)
                        {
                            varStr[i] = ws.Matrices.Item(cnt + 1 + i).SynthesizedName;
                        }
                        break;
                    default:
                        break;
                }

                return varStr;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source + "-" + ex.Message);
                return null;

            }

        }

        public static List<String> TransObjToMtbColList(Object varCols, Mtb.Worksheet ws)
        {
            if (varCols == null || ws == null) return null;

            Type t = varCols.GetType();
            List<String> cols = new List<String>();
            DialogAppraiser da = new DialogAppraiser();
            if (t.IsArray)
            {
                try
                {
                    IEnumerable enumerable = varCols as IEnumerable;
                    foreach (object o in enumerable)
                    {
                        cols.Add(o.ToString());
                    }
                    cols = da.GetMtbCols(cols, ws);

                }
                catch
                {
                    throw new ArgumentException("Invalid input of scale variables");                    
                }

            }
            else if (Type.GetTypeCode(t) == TypeCode.String)
            {
                cols = da.GetMtbColInfo(varCols.ToString());
                cols = da.GetMtbCols(cols, ws);
            }
            else
            {
                cols = null;
            }
            return cols;
        }

        /// <summary>
        /// 在暫存區建立臨時的暫存巨集
        /// </summary>
        /// <param name="fileName">輸入檔名、副檔名</param>
        /// <param name="cmndStr">巨集指令內容</param>
        public static string BuildTemporaryMacro(string fileName, string cmndStr)
        {
            string fullpath;
            string location = 
                System.IO.Path.Combine(Environment.GetEnvironmentVariable("tmp"), 
                "Minitab", 
                "lcy");            
            if (!System.IO.Directory.Exists(location))
                System.IO.Directory.CreateDirectory(location);

            fullpath = System.IO.Path.Combine(location, fileName);

            System.IO.FileStream fs = new System.IO.FileStream(fullpath, System.IO.FileMode.Create);
            fs.Close();

            System.IO.StreamWriter sw;
            sw = new System.IO.StreamWriter(fullpath, false, System.Text.Encoding.Unicode);
            sw.Write(cmndStr);
            sw.Close();
            return fullpath;
        }

        public static string[] GetAllColumnsName(Mtb.Worksheet ws)
        {
            if (ws.Columns.Count == 0)
                return null;
            List<string> colNames = new List<string>();
            foreach (Mtb.Column col in ws.Columns)
            {
                colNames.Add(col.Label);
            }
            return colNames.ToArray();
        }
    }
}
