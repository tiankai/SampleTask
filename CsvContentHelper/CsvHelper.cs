using System;
using System.Text;
using System.Collections.Generic;
// 
using CsvContentHelper.Models;

namespace CsvContentHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class CsvHelper
    {
        private const char splitComma = ',';
        private const char splitDivide = ';';
        private const char newLineChar = '\n';
        private const char enterChar = '\r';
        private const int defaultMaxChars = 800;
        /// <summary>
        /// 
        /// </summary>
        public CsvHelper()
        {
        }

        public bool CheckCsvFileLength(string fileContent, int capacity = Int32.MaxValue - 1)
        {
            var flag = true;
            try
            {
                var sbTemp = new StringBuilder(512, capacity);
                sbTemp.Append(fileContent);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                flag = false;
            }

            return flag;
        }

        public bool CheckCsvFileContent(string fileContent)
        {
            var flag = fileContent.Contains(string.Concat(enterChar, newLineChar)) || fileContent.Contains(enterChar.ToString()) || fileContent.Contains(newLineChar.ToString());
            if (flag == true)
            {
                var rows = fileContent.Split(enterChar, newLineChar);

                return rows.Length > 0;
            }
            else
            {
                return false;
            }
        }

        public bool CheckCsvRowData(string rowData)
        {
            if (string.IsNullOrEmpty(rowData) == false)
            {
                var flag = rowData.Contains(splitComma.ToString()) || rowData.Contains(splitDivide.ToString());
                if (flag == true)
                {
                    var columns_1 = rowData.Split(splitComma);
                    var columns_2 = rowData.Split(splitDivide);

                    return columns_1.Length > 0 || columns_2.Length > 0;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string[] GetCsvAllRows(string fileContent)
        {
            var rows = fileContent.Split(enterChar, newLineChar);

            return rows;
        }

        public string[] GetCsvDataRow(string csvDataRow)
        {
            var columns = new string[] { };
            if (csvDataRow.Contains(splitComma.ToString()))
            {
                columns = csvDataRow.Split(splitComma);

                return columns;
            }
            else if (csvDataRow.Contains(splitDivide.ToString()))
            {
                columns = csvDataRow.Split(splitDivide);
            }

            return columns;
        }

        private MappingStructure GetMappingItem(int columnIndex, string columnValue)
        {
            var item = new MappingStructure
            {
                Order = columnIndex,
                CsvFileHeader = columnValue.Trim(),
                MappedField = string.Empty
            };

            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public List<MappingStructure> GetMappingFields(string fileContent, bool hasHeader, int len = defaultMaxChars)
        {
            var fields = new List<MappingStructure>();
            if (CheckCsvFileLength(fileContent, len) == true)
            {
                if (CheckCsvFileContent(fileContent) == true)
                {
                    var rowIndex = 0;
                    var rows = GetCsvAllRows(fileContent);
                    // 
                    foreach (var r in rows)
                    {
                        if (CheckCsvRowData(r) == true)
                        {
                            var columnIndex = 0;
                            var columns = GetCsvDataRow(r);
                            // 
                            foreach (var c in columns)
                            {
                                if (rowIndex == 0 && hasHeader == true)
                                {
                                    fields.Add(GetMappingItem(columnIndex, c));
                                }
                                columnIndex++;
                            }
                            rowIndex++;
                        }
                    }
                }
            }

            return fields;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileContent"></param>
        /// <param name="hasHeader"></param>
        /// <returns></returns>
        public List<string[]> GetRowData(string fileContent, bool hasHeader, int len = defaultMaxChars)
        {
            var list = new List<string[]>();
            if (CheckCsvFileLength(fileContent, len) == true)
            {
                if (CheckCsvFileContent(fileContent) == true)
                {
                    var rowIndex = 0;
                    var rows = GetCsvAllRows(fileContent);
                    foreach (var r in rows)
                    {
                        if (hasHeader == true && rowIndex == 0)
                        {
                            rowIndex++;
                            continue;
                        }
                        else
                        {
                            if (CheckCsvRowData(r) == true)
                            {
                                var columns = GetCsvDataRow(r);
                                list.Add(columns);
                            }
                            rowIndex++;
                        }
                    }
                }
            }

            return list;
        }
    }
}
