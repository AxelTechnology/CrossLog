using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Log_Library.Interfaces
{
    /// <summary>
    /// Implements the data Log interface.
    /// </summary>
    public interface IDateLog
    {
        /// <summary>
        /// Get or set the date.
        /// </summary>
        DateTime Date { get; set; }
        /// <summary>
        /// Get or set the milliseconds from the beginning of the day.
        /// </summary>
        double FromDayBeginInMilliseconds { get; set; }
    }
}
