using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using System.Windows.Forms;

namespace BoxSystemMange.脚本类
{
    internal class ExcelHelper
    {


        /// <summary>
        /// 从Excel读取数据，只支持单表
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        public static DataTable ReadFromExcel(string FilePath)
        {
            IWorkbook wk = null;
            string extension = System.IO.Path.GetExtension(FilePath); // 获取扩展名
            try
            {
                using (FileStream fs = File.OpenRead(FilePath))
                {
                    if (extension.Equals(".xls")) // 2003
                    {
                        wk = new HSSFWorkbook(fs);
                    }
                    else if (extension.Equals(".xlsx")) // 2007以上
                    {
                        wk = new XSSFWorkbook(fs);
                    }
                    else
                    {
                        throw new Exception("无效的文件格式");
                    }
                }

                // 读取当前表数据
                ISheet sheet = wk.GetSheetAt(0);
                // 构建DataTable
                IRow row = sheet.GetRow(0);
                DataTable result = BuildDataTable(row);
                if (result != null)
                {
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow temp_row = sheet.GetRow(i);
                        if (temp_row == null) continue;
                        List<object> itemArray = new List<object>();
                        for (int j = 0; j < result.Columns.Count; j++)
                        {
                            itemArray.Add(GetValueType(temp_row.GetCell(j)));
                        }
                        result.Rows.Add(itemArray.ToArray());
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                // 输出错误信息
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 从Excel读取数据，支持多表
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        public static DataSet ReadFromExcels(string FilePath)
        {
            DataSet ds = new DataSet();
            IWorkbook wk = null;
            string extension = System.IO.Path.GetExtension(FilePath); //获取扩展名
            try
            {
                using (FileStream fs = File.OpenRead(FilePath))
                {
                    if (extension.Equals(".xls")) //2003
                    {
                        wk = new HSSFWorkbook(fs);
                    }
                    else                         //2007以上
                    {
                        wk = new XSSFWorkbook(fs);
                    }
                }

                int SheetCount = wk.NumberOfSheets;//获取表的数量
                if (SheetCount < 1)
                {
                    return ds;
                }
                for (int s = 0; s < SheetCount; s++)
                {
                    //读取当前表数据
                    ISheet sheet = wk.GetSheetAt(s);
                    //构建DataTable
                    IRow row = sheet.GetRow(0);
                    if (row == null) { continue; }
                    DataTable tempDT = BuildDataTable(row);
                    tempDT.TableName = wk.GetSheetName(s);
                    if (tempDT != null)
                    {
                        if (sheet.LastRowNum >= 1)
                        {
                            for (int i = 1; i < sheet.LastRowNum + 1; i++)
                            {
                                IRow temp_row = sheet.GetRow(i);
                                if (temp_row == null) { continue; }//2019-01-14 修复 行为空时会出错
                                List<object> itemArray = new List<object>();
                                for (int j = 0; j < tempDT.Columns.Count; j++)//解决Excel超出DataTable列问题    lqwvje20181027
                                {
                                    itemArray.Add(GetValueType(temp_row.GetCell(j)));//解决 导入Excel  时间格式问题  lqwvje 20180904
                                }
                                tempDT.Rows.Add(itemArray.ToArray());
                            }
                        }
                        ds.Tables.Add(tempDT);
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <param name="fileName">导出的文件途径</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static int DataTableToExcel(DataTable data, string sheetName, string fileName, bool isColumnWritten = true)
        {
            IWorkbook workbook = null;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook = new XSSFWorkbook();
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                {
                    workbook = new HSSFWorkbook();
                }
                if (workbook == null) { return -1; }

                try
                {
                    ISheet sheet = workbook.CreateSheet(sheetName);
                    int count = 0;
                    if (isColumnWritten) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (int j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                        }
                        count = 1;
                    }

                    for (int i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (int j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }
                        count++;
                    }
                    workbook.Write(fs); //写入到excel

                    return count;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return -1;
                }
            }
        }
        /// <summary>
        /// 将DataSet数据导入到excel中   每个datatable一个sheet,sheet名为datatable名
        /// </summary>
        /// <param name="ds">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="fileName">导出的文件途径</param>
        public static bool DataTableToExcel(DataSet ds, string fileName, bool isColumnWritten = true)
        {
            if (ds == null || ds.Tables.Count < 1)
            {
                return false;
            }
            IWorkbook workbook = null;
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                {
                    workbook = new XSSFWorkbook();
                }
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                {
                    workbook = new HSSFWorkbook();
                }
                if (workbook == null) { return false; }
                try
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        ISheet sheet = workbook.CreateSheet(dt.TableName);
                        if (isColumnWritten) //写入DataTable的列名
                        {
                            IRow row = sheet.CreateRow(0);
                            for (int j = 0; j < dt.Columns.Count; ++j)
                            {
                                row.CreateCell(j).SetCellValue(dt.Columns[j].ColumnName);
                            }
                        }

                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            IRow row = sheet.CreateRow(isColumnWritten ? i + 1 : i);
                            for (int j = 0; j < dt.Columns.Count; ++j)
                            {
                                row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }
                    }
                    workbook.Write(fs); //写入到excel
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return false;
                }
            }
            return true;
        }

        private static DataTable BuildDataTable(IRow Row)
        {
            DataTable result = null;
            if (Row.Cells.Count > 0)
            {
                result = new DataTable();
                for (int i = 0; i < Row.LastCellNum; i++)
                {
                    if (Row.GetCell(i) != null)
                    {
                        result.Columns.Add(Row.GetCell(i).ToString());
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank: //BLANK:  
                    return null;
                case CellType.Boolean: //BOOLEAN:  
                    return cell.BooleanCellValue;
                case CellType.Numeric: //NUMERIC:  
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    return cell.NumericCellValue;
                case CellType.String: //STRING:  
                    return cell.StringCellValue;
                case CellType.Error: //ERROR:  
                    return cell.ErrorCellValue;
                case CellType.Formula: //FORMULA:  
                    cell.SetCellType(CellType.String);
                    return cell.StringCellValue;
                default:
                    return "=" + cell.CellFormula;
            }
        }

    }
}
