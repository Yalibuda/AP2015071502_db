using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace LCY_Database.ReportBuilder
{
    public static class XlReportBuilder
    {
        public static void CreateExperimentTable(ExperimentTable exp, Excel.Application excel)
        {
            //每個 sheet 以 seq_id 命名
            if (exp.SeqID == "") return;
            Excel._Worksheet xlsht = excel.ActiveWorkbook.Worksheets.Add();
            xlsht.Name = exp.SeqID;
            xlsht.Activate();

            //標題
            Excel.Range rng;
            rng = xlsht.Range[xlsht.Cells[1, 1], xlsht.Cells[1, 15]];
            rng.Style.Font.Size = 12;//整個表格的字型大小
            rng.Style.Font.Name = "微軟正黑體";//整個表格的字型
            rng.ColumnWidth = 6f;
            rng.Value = exp.CompTool.Name + string.Format(@"混鍊加工/物性檢測單({0})", exp.SeqID);
            rng.Merge();
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            rng.Font.Size = 14;//選取範圍的字型大小
            rng.Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleDouble;
            xlsht.get_Range("A:A").ColumnWidth = 7.5;
            //rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlDouble;

            //實驗單基本資訊(欄位名)
            rng = xlsht.Range[xlsht.Cells[2, 1], xlsht.Cells[7, 1]];
            rng.Value = excel.WorksheetFunction.Transpose(new string[] { 
                "工單號", "申請日", "申請人", "建議試驗日期", "客戶名稱", "試驗目的" });
            for (int i = 2; i <= 7; i++)
            {
                rng = xlsht.Range[xlsht.Cells[i, 1], xlsht.Cells[i, 2]];
                rng.Merge();
            }

            //填入申請日~實驗目的資料
            xlsht.get_Range("C2").NumberFormat = "@";
            rng = xlsht.Range[xlsht.Cells[3, 3], xlsht.Cells[7, 3]];
            rng.Value = excel.WorksheetFunction.Transpose(new string[] {
                exp.ApplyDate, exp.User.Value, exp.RunDate, exp.Customer.Value, exp.Purpose });

            for (int i = 2; i <= 4; i++) //把工單號~ 申請人的資訊 merge，並且加入表格底線
            {
                rng = xlsht.Range[xlsht.Cells[i, 3], xlsht.Cells[i, 5]];
                rng.Merge();
                rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
                rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            for (int i = 5; i <= 6; i++) //把試驗日期~ 客戶名稱的資訊 merge，並且加入表格底線
            {
                rng = xlsht.Range[xlsht.Cells[i, 3], xlsht.Cells[i, 7]];
                rng.Merge();
                rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
                rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            //實驗目的資訊欄位 merge + 底線
            rng = xlsht.Range[xlsht.Cells[7, 3], xlsht.Cells[7, 8]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;


            xlsht.Cells[4, 6].Value = "混鍊試驗期限 :";
            rng = xlsht.Range[xlsht.Cells[4, 8], xlsht.Cells[4, 10]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

            xlsht.Cells[4, 11].Value = "分析試驗期限 :";
            rng = xlsht.Range[xlsht.Cells[4, 13], xlsht.Cells[4, 14]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

            xlsht.Cells[5, 8].Value = "實際實驗日期";
            rng = xlsht.Range[xlsht.Cells[5, 10], xlsht.Cells[5, 14]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

            xlsht.Cells[6, 8].Value = "試料牌號";
            rng = xlsht.Range[xlsht.Cells[6, 10], xlsht.Cells[6, 14]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsht.Cells[6, 10].Value = exp.Grade;

            //Modify: 以試料需求顯示總重
            //xlsht.Cells[7, 11].Value = "試料需求";
            //xlsht.Cells[7, 13].Value = Convert.ToDouble(exp.BagNumber) * Convert.ToDouble(exp.BagWeight);
            //rng = xlsht.Range[xlsht.Cells[7, 13], xlsht.Cells[7, 13]];
            //rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            //xlsht.Cells[7, 14].Value = "Kg";
            xlsht.Cells[7, 9].Value = "袋重";
            xlsht.Cells[7, 10].Value = Convert.ToDouble(exp.BagWeight);
            xlsht.Cells[7, 11].Value = "Kg";
            xlsht.Cells[7, 12].Value = "數量";
            xlsht.Cells[7, 13].Value = Convert.ToDouble(exp.BagNumber);
            xlsht.Cells[7, 14].Value = "袋";
            rng = xlsht.Range[xlsht.Cells[7, 10], xlsht.Cells[7, 10]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng = xlsht.Range[xlsht.Cells[7, 13], xlsht.Cells[7, 13]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;


            xlsht.Cells[2, 7].Value = "保留樣品";
            xlsht.Cells[2, 10].Value = "陪同試驗";
            xlsht.Cells[2, 13].Value = "急件";
            xlsht.Cells[3, 7].Value = "材質證明";
            xlsht.Cells[3, 10].Value = "分析報告";
            xlsht.Cells[3, 13].Value = "自我聲明";
            rng = xlsht.get_Range("G2,G3,J2,J3,M2,M3");
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            rng = xlsht.get_Range("F2,F3,I2,I3,L2,L3");
            rng.Value = "□";
            rng.Font.Name = "Times New Roman";
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;




            //int rowsOfPart2 = Math.Max(exp.CompTool.Items.Count(), exp.Formulas.Count());

            //
            // 配方項目
            //            
            string[] r_item_name = exp.Formulas.Select(x => x.Name).ToArray();
            string[] r_item_value = exp.Formulas.Select(x => x.Value).ToArray();
            string[] r_item_unit = exp.Formulas.Select(x => x.Unit).ToArray();
            int _row = 10, _col = 1;
            xlsht.Cells[_row, 1].Value = "試驗配方";
            xlsht.Cells[_row, 3].Value = "值";
            xlsht.Cells[_row, 4].Value = "單位";
            xlsht.Cells[_row, 5].Value = "單重";
            xlsht.Cells[_row, 6].Value = "總重量";
            rng = xlsht.Range[xlsht.Cells[_row, 1], xlsht.Cells[_row, 6]];
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;//表頭加入底線
            //
            // 填入配方項目名稱+單位+值
            //
            _col = 1;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + exp.Formulas.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(r_item_name);
            _col = 3;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + exp.Formulas.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(r_item_value);
            _col = 4;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + exp.Formulas.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(r_item_unit);

            //
            // 將比重轉換成實際重量
            //
            List<double> actualwt = new List<double>();

            //
            // 計算單重
            //
            double singlewt = Convert.ToDouble(exp.BagWeight);
            foreach (IItem item in exp.Formulas)
            {
                double p = Convert.ToDouble(item.Value);
                actualwt.Add(p * singlewt / 100);
            }
            _col = 5;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + exp.Formulas.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(actualwt.ToArray());
            xlsht.Cells[_row + exp.Formulas.Count() + 1, _col].Value = actualwt.Sum();//計算單次實驗總重 

            //
            // 計算總重
            //
            actualwt.Clear(); //清除之前單重的計算結果
            double totalwt = Convert.ToDouble(exp.BagNumber) * Convert.ToDouble(exp.BagWeight);
            foreach (IItem item in exp.Formulas)
            {
                double p = Convert.ToDouble(item.Value);
                actualwt.Add(p * totalwt / 100);
            }
            _col = 6;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + exp.Formulas.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(actualwt.ToArray());
            xlsht.Cells[_row + exp.Formulas.Count() + 1, _col].Value = actualwt.Sum();//計算所有試驗總重

            //
            // 結算列的格式調整
            //
            xlsht.Cells[_row + exp.Formulas.Count() + 1, _col - 2].Value = "total:";
            rng = xlsht.Range[xlsht.Cells[_row + exp.Formulas.Count() + 1, 1], xlsht.Cells[_row + exp.Formulas.Count() + 1, 6]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;//結算加入上底線

            //
            // 混練條件
            //
            xlsht.Cells[_row, 10].Value = "混練加工條件";
            xlsht.Cells[_row, 10].Font.Color = Excel.XlRgbColor.rgbDarkRed;
            xlsht.Cells[_row, 10].Font.Bold = true;
            rng = xlsht.Range[xlsht.Cells[_row, 10], xlsht.Cells[_row, 13]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;//結算加入上底線

            List<IItem> c_items = exp.CompTool.Items.Where(x => x.Flag != "CT").OrderBy(x => x.Item_Seq).ToList();
            string[] c_item_name = c_items.Select(x => x.Name).ToArray();
            string[] c_item_unit = c_items.Select(x => x.Unit).ToArray();
            string[] c_item_value = c_items.Select(x => x.Value).ToArray();
            _col = 10;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + c_item_name.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(c_item_name);
            _col = 12;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + c_item_name.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(c_item_value);
            _col = 13;
            rng = xlsht.Range[xlsht.Cells[_row + 1, _col], xlsht.Cells[_row + c_item_name.Count(), _col]];
            rng.Value = excel.WorksheetFunction.Transpose(c_item_unit);

            //
            // 區段溫度
            //
            List<IItem> ct_items = exp.CompTool.Items.Where(x => x.Flag == "CT").OrderBy(x => x.Item_Seq).ToList();
            string[] ct_item_name = ct_items.Select(x => x.Name).ToArray();
            string[] ct_item_value = ct_items.Select(x => x.Value).ToArray();
            double m_ct = Math.Ceiling(((double)ct_item_name.Length) / 14); //判斷區段溫度項目是否超過頁面
            _row = _row + Math.Max(c_item_name.Count(), r_item_name.Count()) + 1;//區段溫度表格起始列
            xlsht.Cells[_row, 2].Value = "押出機-區段溫度";
            xlsht.Cells[_row, 2].Font.Bold = true;
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 3 * m_ct, 1]];
            rng.Value = excel.WorksheetFunction.Transpose(new string[] { "", "設定溫度", "實際溫度" });
            _col = 2;
            for (int i = 0; i < m_ct; i++)
            {
                string[] sub_ct_item_name = ct_item_name.Where((x, j) => j >= 13 * i & j < 13 * (i + 1)).ToArray();
                string[] sub_ct_item_value = ct_item_value.Where((x, j) => j >= 13 * i & j < 13 * (i + 1)).ToArray();
                rng = xlsht.Range[xlsht.Cells[_row + 1 + 3 * i, _col], xlsht.Cells[_row + 1 + 3 * i, _col + sub_ct_item_name.Count() - 1]];
                rng.Value = sub_ct_item_name;
                rng.Interior.Color = Excel.XlRgbColor.rgbLightGray;
                rng = xlsht.Range[xlsht.Cells[_row + 2 + 3 * i, _col], xlsht.Cells[_row + 2 + 3 * i, _col + sub_ct_item_value.Count() - 1]];
                rng.Value = sub_ct_item_value;
            }

            //押出機-區段溫度表格 + 框線
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 3 * m_ct, Math.Min(13, ct_item_name.Length) + 1]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;

            //
            // 射出加工
            //
            _row = _row + (int)m_ct * 3 + 2;// 射出加工表格起史位置
            xlsht.Cells[_row, 1].Value = "射出加工條件";
            xlsht.Cells[_row, 1].Font.Color = Excel.XlRgbColor.rgbDarkRed;
            xlsht.Cells[_row, 1].Font.Bold = true;
            List<IItem> i_item = exp.InjTool.Items.Where(x => x.Flag != "IT").OrderBy(x => x.Item_Seq).ToList();
            string[] i_item_name = i_item.Select(x => x.Name).ToArray();
            string[] i_item_value = i_item.Select(x => x.Value).ToArray();
            string[] i_item_unit = i_item.Select(x => x.Unit).ToArray();
            double m_i_item = Math.Ceiling(((double)i_item_name.Length) / 3);//判斷單欄需要多少列, 設計頁寬約放三欄..
            _col = 1;
            for (int i = 0; i < 3; i++)// by column 處理
            {
                string[] sub_i_item_name = i_item_name.Where((x, j) => j >= i * m_i_item & j < (i + 1) * (m_i_item)).ToArray();
                string[] sub_i_item_value = i_item_value.Where((x, j) => j >= i * m_i_item & j < (i + 1) * (m_i_item)).ToArray();
                string[] sub_i_item_unit = i_item_unit.Where((x, j) => j >= i * m_i_item & j < (i + 1) * (m_i_item)).ToArray();
                rng = xlsht.Range[xlsht.Cells[_row + 1, _col + i * 4], xlsht.Cells[_row + sub_i_item_name.Count(), _col + 1 + i * 4]];
                rng.Value = excel.WorksheetFunction.Transpose(sub_i_item_name);
                foreach (Excel.Range subrng in rng.Rows)
                {
                    subrng.Merge();
                }
                rng = xlsht.Range[xlsht.Cells[_row + 1, _col + i * 4 + 2], xlsht.Cells[_row + sub_i_item_name.Count(), _col + i * 4 + 2]];
                rng.Value = excel.WorksheetFunction.Transpose(sub_i_item_value);
                foreach (Excel.Range subrng in rng.Rows)
                {
                    subrng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
                }
                rng = xlsht.Range[xlsht.Cells[_row + 1, _col + i * 4 + 3], xlsht.Cells[_row + sub_i_item_name.Count(), _col + i * 4 + 3]];
                rng.Value = excel.WorksheetFunction.Transpose(sub_i_item_unit);
            }

            //
            // 射出機溫度
            //
            List<IItem> it_item = exp.InjTool.Items.Where(x => x.Flag == "IT").OrderBy(x => x.Item_Seq).ToList();
            string[] it_item_name = it_item.Select(x => x.Name).ToArray();
            string[] it_item_value = it_item.Select(x => x.Value).ToArray();
            string[] it_item_unit = it_item.Select(x => x.Unit).ToArray();
            double m_it = Math.Ceiling(((double)it_item_name.Length) / 13); //判斷區段溫度項目是否超過頁面
            _row = _row + (int)m_i_item + 2;//區段溫度表格起始列
            xlsht.Cells[_row, 1].Value = "射出溫度";
            xlsht.Cells[_row, 1].Font.Color = Excel.XlRgbColor.rgbDarkRed;
            xlsht.Cells[_row, 1].Font.Bold = true;
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 3 * m_it, 1]];
            rng.Value = excel.WorksheetFunction.Transpose(new string[] { "", "設定溫度", "實際溫度" });
            _col = 2;
            for (int i = 0; i < m_it; i++)
            {
                string[] sub_it_item_name = it_item_name.Where((x, j) => j >= 13 * i & j < 13 * (i + 1)).ToArray();
                string[] sub_it_item_value = it_item_value.Where((x, j) => j >= 13 * i & j < 13 * (i + 1)).ToArray();
                rng = xlsht.Range[xlsht.Cells[_row + 1 + i * 3, _col], xlsht.Cells[_row + 1 + i * 3, _col + sub_it_item_name.Count() - 1]];
                rng.Interior.Color = Excel.XlRgbColor.rgbLightGray;
                rng.Value = sub_it_item_name;
                rng = xlsht.Range[xlsht.Cells[_row + 2 + i * 3, _col], xlsht.Cells[_row + 2 + i * 3, _col + sub_it_item_value.Count() - 1]];
                rng.Value = sub_it_item_value;
            }

            //押出機-區段溫度表格 + 框線
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 3 * m_it, Math.Min(13, it_item_name.Length) + 1]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;

            //
            // 備註欄
            //
            _row = _row + (int)m_it * 3 + 2;
            xlsht.Cells[_row, 1].Value = "實驗備註";
            rng = xlsht.Range[xlsht.Cells[_row, 1], xlsht.Cells[_row, 15]];
            rng.Merge();
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            xlsht.Cells[_row + 1, 1].Value = exp.Remark;
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 5, 15]];
            rng.Merge();
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //備註欄位框線
            rng = xlsht.Range[xlsht.Cells[_row, 1], xlsht.Cells[_row + 5, 15]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;

            //
            // 物性
            //
            _row = _row + 7;
            _col = 1;
            xlsht.Cells[_row, 1].Value = "物性檢測數據";
            xlsht.Cells[_row, 1].Font.Color = Excel.XlRgbColor.rgbDarkRed;
            xlsht.Cells[_row, 1].Font.Bold = true;
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 1, 12]];
            rng.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            rng.Value = new string[] { "檢測項目", "", "單位", "", "檢測值(平均)", "", "", "", "偏差值", "", "", "" };
            List<IItem> property = exp.Property.OrderBy(x => x.ProdType).ToList();
            string[] p_item_name = property.Select(x => x.Name).ToArray();
            string[] p_item_unit = property.Select(x => x.Unit).ToArray();
            rng = xlsht.Range[xlsht.Cells[_row + 2, 1], xlsht.Cells[_row + p_item_name.Length + 1, 1]];
            rng.Value = excel.WorksheetFunction.Transpose(p_item_name);
            rng = xlsht.Range[xlsht.Cells[_row + 2, 3], xlsht.Cells[_row + p_item_name.Length + 1, 3]];
            rng.Value = excel.WorksheetFunction.Transpose(p_item_unit);
            for (int i = _row + 1; i < _row + p_item_name.Length + 2; i++)
            {
                rng = xlsht.Range[xlsht.Cells[i, 1], xlsht.Cells[i, 2]];
                rng.Merge();
                rng = xlsht.Range[xlsht.Cells[i, 3], xlsht.Cells[i, 4]];
                rng.Merge();
                rng = xlsht.Range[xlsht.Cells[i, 5], xlsht.Cells[i, 8]];
                rng.Merge();
                rng = xlsht.Range[xlsht.Cells[i, 9], xlsht.Cells[i, 12]];
                rng.Merge();
            }
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + p_item_name.Length + 1, 12]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;


            //
            // 版面設定
            //
            try
            {
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.TopMargin = excel.InchesToPoints(0.59);
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.BottomMargin = excel.InchesToPoints(0.59);
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.LeftMargin = excel.InchesToPoints(0.354);
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.RightMargin = excel.InchesToPoints(0.354);
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.CenterHorizontally = true;
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.Zoom = false;
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.FitToPagesWide = 1;
                ((Excel._Worksheet)excel.ActiveSheet).PageSetup.FitToPagesTall = false;
            }
            catch (Exception ex)
            {
                //針對 lcy 電腦設置的防呆機制..當使用 CutePDF Writer 導致無法使用 PageSetup 屬性
            }



        }
        public static void CreateExperimentTable_Competitor(ExperimentTable exp, Excel.Application excel)
        {
            //每個 sheet 以 seq_id 命名
            if (exp.SeqID == "") return;
            Excel._Worksheet xlsht = excel.ActiveWorkbook.Worksheets.Add();
            xlsht.Name = exp.SeqID;
            xlsht.Activate();

            //標題
            Excel.Range rng;
            rng = xlsht.Range[xlsht.Cells[1, 1], xlsht.Cells[1, 15]];
            rng.Style.Font.Size = 12;//整個表格的字型大小
            rng.Style.Font.Name = "微軟正黑體";//整個表格的字型
            rng.ColumnWidth = 6f;
            rng.Value = exp.CompTool.Name + @"混鍊加工/物性檢測單";
            rng.Merge();
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            rng.Font.Size = 14;//選取範圍的字型大小
            rng.Font.Underline = Excel.XlUnderlineStyle.xlUnderlineStyleDouble;
            xlsht.get_Range("A:A").ColumnWidth = 7.5;
            //rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlDouble;

            //實驗單基本資訊(欄位名)
            rng = xlsht.Range[xlsht.Cells[2, 1], xlsht.Cells[7, 1]];
            rng.Value = excel.WorksheetFunction.Transpose(new string[] { 
                "工單號", "申請日", "申請人", "建議試驗日期", "客戶名稱", "試驗目的" });
            for (int i = 2; i <= 7; i++)
            {
                rng = xlsht.Range[xlsht.Cells[i, 1], xlsht.Cells[i, 2]];
                rng.Merge();
            }

            //填入工單號~實驗目的資料
            rng = xlsht.Range[xlsht.Cells[2, 3], xlsht.Cells[7, 3]];
            rng.Value = excel.WorksheetFunction.Transpose(new string[] {
                exp.SeqID, exp.ApplyDate, exp.User.Value, exp.RunDate, exp.Customer.Value, exp.Purpose });

            for (int i = 2; i <= 4; i++) //把工單號~ 客戶名稱的資訊 merge，並且加入表格底線
            {
                rng = xlsht.Range[xlsht.Cells[i, 3], xlsht.Cells[i, 5]];
                rng.Merge();
                rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
                rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            for (int i = 5; i <= 6; i++) //把工單號~ 客戶名稱的資訊 merge，並且加入表格底線
            {
                rng = xlsht.Range[xlsht.Cells[i, 3], xlsht.Cells[i, 7]];
                rng.Merge();
                rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
                rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            //實驗目的資訊欄位 merge + 底線
            rng = xlsht.Range[xlsht.Cells[7, 3], xlsht.Cells[7, 10]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;


            xlsht.Cells[4, 6].Value = "混鍊試驗期限 :";
            rng = xlsht.Range[xlsht.Cells[4, 8], xlsht.Cells[4, 10]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

            xlsht.Cells[4, 11].Value = "分析試驗期限 :";
            rng = xlsht.Range[xlsht.Cells[4, 13], xlsht.Cells[4, 14]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

            xlsht.Cells[5, 8].Value = "實際實驗日期";
            rng = xlsht.Range[xlsht.Cells[5, 10], xlsht.Cells[5, 14]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;

            xlsht.Cells[6, 8].Value = "試料牌號";
            rng = xlsht.Range[xlsht.Cells[6, 10], xlsht.Cells[6, 14]];
            rng.Merge();
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsht.Cells[6, 10].Value = exp.Grade;

            xlsht.Cells[7, 11].Value = "試料需求";
            xlsht.Cells[7, 13].Value = Convert.ToDouble(exp.BagNumber) * Convert.ToDouble(exp.BagWeight);
            rng = xlsht.Range[xlsht.Cells[7, 13], xlsht.Cells[7, 13]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            xlsht.Cells[7, 14].Value = "Kg";

            xlsht.Cells[2, 7].Value = "保留樣品";
            xlsht.Cells[2, 10].Value = "陪同試驗";
            xlsht.Cells[2, 13].Value = "急件";
            xlsht.Cells[3, 7].Value = "材質證明";
            xlsht.Cells[3, 10].Value = "分析報告";
            xlsht.Cells[3, 13].Value = "自我聲明";
            rng = xlsht.get_Range("G2,G3,J2,J3,M2,M3");
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            rng = xlsht.get_Range("F2,F3,I2,I3,L2,L3");
            rng.Value = "□";
            rng.Font.Name = "Times New Roman";
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;




            //int rowsOfPart2 = Math.Max(exp.CompTool.Items.Count(), exp.Formulas.Count());

            //
            // 配方項目、混練加工、押出機溫度、射出加工、射出溫度省略
            //            

            int _row = 10, _col = 1;
            //
            // 備註欄
            //            
            xlsht.Cells[_row, 1].Value = "實驗備註";
            rng = xlsht.Range[xlsht.Cells[_row, 1], xlsht.Cells[_row, 15]];
            rng.Merge();
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            xlsht.Cells[_row + 1, 1].Value = exp.Remark;
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 5, 15]];
            rng.Merge();
            rng.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //備註欄位框線
            rng = xlsht.Range[xlsht.Cells[_row, 1], xlsht.Cells[_row + 5, 15]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;

            //
            // 物性
            //
            _row = _row + 7;
            _col = 1;
            xlsht.Cells[_row, 1].Value = "物性檢測數據";
            xlsht.Cells[_row, 1].Font.Color = Excel.XlRgbColor.rgbDarkRed;
            xlsht.Cells[_row, 1].Font.Bold = true;
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + 1, 12]];
            rng.Interior.Color = Excel.XlRgbColor.rgbLightGray;
            rng.Value = new string[] { "檢測項目", "", "單位", "", "檢測值(平均)", "", "", "", "偏差值", "", "", "" };
            List<IItem> property = exp.Property.OrderBy(x => x.ProdType).ToList();
            string[] p_item_name = property.Select(x => x.Name).ToArray();
            string[] p_item_unit = property.Select(x => x.Unit).ToArray();
            rng = xlsht.Range[xlsht.Cells[_row + 2, 1], xlsht.Cells[_row + p_item_name.Length, 1]];
            rng.Value = excel.WorksheetFunction.Transpose(p_item_name);
            rng = xlsht.Range[xlsht.Cells[_row + 2, 3], xlsht.Cells[_row + p_item_name.Length, 3]];
            rng.Value = excel.WorksheetFunction.Transpose(p_item_unit);
            for (int i = _row + 1; i < _row + p_item_name.Length + 2; i++)
            {
                rng = xlsht.Range[xlsht.Cells[i, 1], xlsht.Cells[i, 2]];
                rng.Merge();
                rng = xlsht.Range[xlsht.Cells[i, 3], xlsht.Cells[i, 4]];
                rng.Merge();
                rng = xlsht.Range[xlsht.Cells[i, 5], xlsht.Cells[i, 8]];
                rng.Merge();
                rng = xlsht.Range[xlsht.Cells[i, 9], xlsht.Cells[i, 12]];
                rng.Merge();
            }
            rng = xlsht.Range[xlsht.Cells[_row + 1, 1], xlsht.Cells[_row + p_item_name.Length + 1, 12]];
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            rng.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;


            //
            // 版面設定
            //
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.Zoom = false;
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.FitToPagesWide = 1;
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.FitToPagesTall = false;
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.TopMargin = excel.InchesToPoints(0.59);
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.BottomMargin = excel.InchesToPoints(0.59);
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.LeftMargin = excel.InchesToPoints(0.354);
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.RightMargin = excel.InchesToPoints(0.354);
            ((Excel._Worksheet)excel.ActiveSheet).PageSetup.CenterHorizontally = true;

        }
        public static void CreateExperimentUploadTable(ExperimentTable exp, Excel.Application excel)
        {
            //每個 sheet 以 seq_id 命名
            if (exp.SeqID == "") return;
            Excel._Worksheet xlsht = excel.ActiveWorkbook.Worksheets.Add();
            xlsht.Name = exp.SeqID + "_Upload";
            xlsht.Activate();

            //
            // 取得所有 item
            //
            List<IItem> _totalItems = new List<IItem>();
            if (exp.CompTool.Items != null) _totalItems = _totalItems.Union(exp.CompTool.Items).ToList();
            if (exp.InjTool.Items != null) _totalItems = _totalItems.Union(exp.InjTool.Items).ToList();
            if (exp.Formulas != null) _totalItems = _totalItems.Union(exp.Formulas).ToList();
            if (exp.Property != null) _totalItems = _totalItems.Union(exp.Property).ToList();
            string[] item_flag = new string[] { "C1", "CT", "I1", "IT", "P" };
            IItem[] uploadItems = _totalItems.Where(x => item_flag.Contains(x.Flag)).ToArray();
            DataTable dt = new DataTable();
            string[] col_dt = new string[] { "seq_id", "lot_id", "item", "item_flag", "unit", "item_value" };
            foreach (string col in col_dt)
            {
                dt.Columns.Add(col);
                dt.Columns[col].DataType = typeof(string);
            }

            foreach (IItem item in uploadItems)
            {
                DataRow dr = dt.NewRow();
                dr["seq_id"] = exp.SeqID;
                dr["lot_id"] = "";
                dr["item_value"] = "";
                dr["item_flag"] = item.Flag;
                dr["item"] = item.Name;
                dr["unit"] = item.Unit;
                dt.Rows.Add(dr);
            }
            Excel.Range rng;
            int rows = dt.Rows.Count;
            xlsht.get_Range("A1").Value = "Remark:";
            xlsht.get_Range("B1").Value = exp.Remark;
            rng = xlsht.Range[xlsht.Cells[2, 1], xlsht.Cells[2, 6]];
            rng.Value = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                rng = xlsht.Range[xlsht.Cells[3, i + 1], xlsht.Cells[2 + rows, i + 1]];
                rng.NumberFormat = "@";
                string[] values = dt.Rows.Cast<DataRow>().Select(x => x[i].ToString()).ToArray();
                rng.Value = excel.WorksheetFunction.Transpose(values);
            }

        }

        /// <summary>
        /// 把 DataTable 以新增 Sheet 的方式插入 Excel workbook 中，並且於第一列加入 Remark 欄位
        /// </summary>
        /// <param name="dt">某一個 seq_id 的實驗上傳表，Table 中包含 seq_id, lot_id, item, item_flag, unit,item_value, remark </param>
        /// <param name="excel">Excel application</param>
        public static void CreateExperimentUploadTable(DataTable dt, Excel.Application excel)
        {
            try
            {
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                //於最後面新增一個工作表
                Excel._Worksheet xlsht = excel.ActiveWorkbook.Worksheets.Add(After: excel.Sheets[excel.Sheets.Count]);
                xlsht.Name = dt.TableName; //給定表格名稱

                //指定第一欄為文字格式..考量 seq_id and remark
                xlsht.get_Range("A:A").NumberFormat = "@";

                int row = 1; //紀錄位置
                xlsht.Cells[row, 1] = "Remark";
                string remark = dt.Rows[0]["remark"].ToString(); //取得 remark 資訊
                if (!string.IsNullOrWhiteSpace(remark)) xlsht.Cells[row, 2] = remark;

                row = 2; //column heading
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.ToLower() != "remark")
                        xlsht.Cells[row, (i + 1)] = dt.Columns[i].ColumnName;
                }

                row = 3; //data rows
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < dt.Columns.Count; j++) // 不含 remark 欄位
                    {
                        if (dt.Columns[j].ColumnName.ToLower() != "remark")
                            xlsht.Cells[(row + i), (j + 1)] = dt.Rows[i][j];
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
