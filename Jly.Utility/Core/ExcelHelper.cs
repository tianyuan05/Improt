using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Jly.Utility.Core
{
    /// <summary>
    /// Excel数据读取类
    /// </summary>
    public class ExcelHelper
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
                    IEnumerable<DocumentFormat.OpenXml.Spreadsheet.Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<DocumentFormat.OpenXml.Spreadsheet.Row>();

                    Type type = typeof(T);
                    var properties = type.GetProperties();

                    Console.WriteLine($"ROWS={rows.Count()}");
                    foreach (var row in rows)
                    {
                        //列的个数与ExcelModel属性个数相等则判断为符合数据格式
                        if (row?.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>()?.Count() != properties.Count())
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
                        foreach (DocumentFormat.OpenXml.Spreadsheet.Cell cell in row?.Descendants<DocumentFormat.OpenXml.Spreadsheet.Cell>())
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
                return result;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        /// <summary>
        /// 保存集合到Excel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">数据源</param>
        /// <param name="totalRows">记录函数</param>
        /// <param name="filePath">excel文件保存路径</param>
        public static void Writer<T>(IEnumerable<T> data, int totalRows, string filePath)
        {
            Type dataType = typeof(T);

            var columnDefinitons = GetColumnDefinitons(dataType);
            var rows = GetRows<T>(columnDefinitons, data, totalRows);

            var worksheetDfn = new WorksheetDfn
            {
                TableName = dataType.Name,
                Name = dataType.Name,
                ColumnHeadings = columnDefinitons,
                Rows = rows
            };

            var workbookDfn = new WorkbookDfn { Worksheets = new[] { worksheetDfn } };

            SpreadsheetWriter.Write(filePath, workbookDfn);
        }

        private static List<CellDfn> GetColumnDefinitons(Type dataType)
        {
            List<CellDfn> columnDefinitons = new List<CellDfn>();
            PropertyInfo[] properties = dataType.GetProperties();

            foreach (PropertyInfo column in properties)
            {
                columnDefinitons.Add(new CellDfn()
                {
                    CellDataType = CellDataType.String,
                    Value = column.Name,
                    Bold = true
                });
            }

            return columnDefinitons;
        }

        private static RowDfn[] GetRows<T>(List<CellDfn> columnHeadings, IEnumerable<T> data, int totalRows)
        {
            if (columnHeadings == null) throw new ArgumentNullException();
            if (data == null) throw new ArgumentNullException();

            var dataType = typeof(T);
            PropertyInfo[] properties = dataType.GetProperties();

            var rows = new OpenXmlPowerTools.RowDfn[totalRows];
            var currentRow = 0;
            foreach (var dataEntry in data)
            {
                var currentColumn = 0;
                var row = new OpenXmlPowerTools.RowDfn();

                OpenXmlPowerTools.CellDfn[] cells = new OpenXmlPowerTools.CellDfn[columnHeadings.Count];

                foreach (var column in columnHeadings)
                {
                    var property = dataType.GetProperty(column.Value.ToString());
                    var value = property.GetValue(dataEntry);

                    var tempValue = string.Empty;
                    if (value != null)
                    {
                        tempValue = value.ToString();
                    }

                    cells[currentColumn] = new OpenXmlPowerTools.CellDfn
                    {
                        Value = tempValue,
                        CellDataType = GetOpenXmlExcelType(properties[currentColumn].PropertyType)
                    };

                    currentColumn++;
                }

                row.Cells = cells;

                rows[currentRow] = row;

                currentRow++;
            }

            return rows;
        }

        private static CellDataType GetOpenXmlExcelType(Type type)
        {
            if (type == typeof(short) || type == typeof(short?) ||
                type == typeof(decimal) || type == typeof(decimal?) ||
                type == typeof(double) || type == typeof(double?) ||
                type == typeof(float) || type == typeof(float?) ||
                type == typeof(int) || type == typeof(int?) ||
                type == typeof(long) || type == typeof(long?)) return CellDataType.Number;

            if (type == typeof(bool) || type == typeof(bool?)) return CellDataType.Boolean;
            if (type == typeof(DateTime) || type == typeof(DateTime?)) return CellDataType.Date;

            return CellDataType.String;
        }


        private static string GetCellValue(SpreadsheetDocument doc, DocumentFormat.OpenXml.Spreadsheet.Cell cell)
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
