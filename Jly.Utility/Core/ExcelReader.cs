using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Core
{
    /// <summary>
    /// Excel数据读取类
    /// </summary>
    public class ExcelReader
    {
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="fileName">Excel的全路径</param>
        /// <param name="hasHeader">Excel是否具有表头：TRUE-有表头，从第二行开始读取数据；FALSE-没有表头，从第一行开始读取数据</param>
        /// <returns><see cref="ExcelModel"/>对象集合</returns>
        public static List<T> Reader<T>(string fileName, bool hasHeader) where T : new()
        {
            List<T> result = new List<T>();
            try
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fileName, false))
                {
                    Sheets sheets = doc.WorkbookPart.Workbook.Sheets;
                    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                    Type type = typeof(T);
                    var properties = type.GetProperties();

                    Console.WriteLine($"ROWS={rows.Count()}");
                    foreach (var row in rows)
                    {
                        //列的个数与ExcelModel属性个数相等则判断为符合数据格式
                        if (row?.Descendants<Cell>()?.Count() != properties.Count())
                        {
                            Console.WriteLine(row.RowIndex.ToString());
                            throw new Exception("导入的文件格式不正确！");
                        }

                        T entity = new T();

                        if (hasHeader)
                        {
                            if (row?.RowIndex.Value == 1)
                                continue;
                        }
                        int i = 0;
                        foreach (Cell cell in row?.Descendants<Cell>())
                        {
                            properties[i].SetValue(entity, GetCellValue(doc, cell));
                            i++;
                        }

                        bool[] bs = new bool[properties.Count()];
                        for (int j = 0; j < properties.Count(); j++)
                        {
                            var value = properties[j].GetValue(entity);
                            if (value == null)
                                bs[j] = true;
                        }
                        if (bs.All(x => x == true)) break;

                        result.Add(entity);
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return result;
        }


        private static string GetCellValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell?.CellValue?.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            if (cell.DataType != null && cell.DataType.Value == CellValues.Date)
            {
                if (DateTime.TryParse(value, out DateTime dt))
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return value;
            }
            if (cell.DataType == null && cell.CellReference.Value.ToUpper().StartsWith("F"))
            {
                if (double.TryParse(value, out double ticks))
                    return DateTime.FromOADate(ticks).ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return value;
            }
            if (cell.DataType == null && cell.CellReference.Value.ToUpper().StartsWith("G"))
            {
                if (double.TryParse(value, out double ticks))
                    return DateTime.FromOADate(ticks).ToString("yyyy-MM-dd HH:mm:ss");
                else
                    return value;
            }
            return value;
        }
    }
}
