using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSV_DynamicParser.Models
{
    /// <summary>
    /// Configurable Data Structure
    /// </summary>
    public class ConfigurationData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTypeEnum DataType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxLength { get; set; }
    }
}
