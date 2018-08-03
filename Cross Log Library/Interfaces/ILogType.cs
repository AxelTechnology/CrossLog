using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Log_Library.Interfaces
{
    /// <summary>
    /// Implements the Log type interface.
    /// </summary>
    public interface ILogType
    {
        /// <summary>
        /// Get or set the log type.
        /// </summary>
        string Type { set; get; }
    }
}
