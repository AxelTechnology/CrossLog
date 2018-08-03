using Cross_Log_Library.Interfaces;
using System;

namespace Cross_Log_Library.Classes
{
    /// <summary>
    /// Implements the DateLog class.
    /// </summary>
    public class DateLog : IDateLog
    {
        #region Properties
        /// <summary>
        /// Get or set the date.
        /// </summary>
        private DateTime date = new DateTime(2018, 1, 1, 1, 1, 1, 1);
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        /// <summary>
        /// Get or set the date from the beginning of the day.
        /// </summary>
        private double fromDayBeginInMilliseconds = 0;
        public double FromDayBeginInMilliseconds
        {
            get
            {
                return fromDayBeginInMilliseconds;
            }
            set
            {
                fromDayBeginInMilliseconds = value;
            }
        }
        #endregion

        #region Constructor
        public DateLog(DateTime date, double fromDayBeginInMilliseconds)
        {
            Date = date;
            FromDayBeginInMilliseconds = fromDayBeginInMilliseconds;
        }
        #endregion
    }
}
