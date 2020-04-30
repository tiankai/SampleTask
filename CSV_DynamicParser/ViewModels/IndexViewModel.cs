using System;
using System.Collections.Generic;
//
using CSV_DynamicParser.Models;
using CsvContentHelper.Models;

namespace CSV_DynamicParser.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string CsvFile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool FileHasHeaders { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int FileHeaderCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MappingStructure> MappedItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string[]> DataSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<ConfigurationData> MappingFieldList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
