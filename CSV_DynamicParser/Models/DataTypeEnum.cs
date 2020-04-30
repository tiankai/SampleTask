using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSV_DynamicParser.Models
{
    /// <summary>
    /// 
    /// </summary>
    public enum DataTypeEnum : uint
    {
        /// <summary>
        /// used for Skip option
        /// </summary>
        None = 0,
        /// <summary>
        /// 
        /// </summary>
        String = 1,
        /// <summary>
        /// 
        /// </summary>
        Decimal = 2,
        /// <summary>
        /// 
        /// </summary>
        Integer = 2
    }
}
