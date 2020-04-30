using System;
using System.Collections.Generic;

namespace CsvContentHelper.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MappingStructure
    {
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CsvFileHeader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MappedField { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsSkip { get; set; }
    }
}
