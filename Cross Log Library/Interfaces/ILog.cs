using Cross_Log_Library.Classes;

namespace Cross_Log_Library.Interfaces
{
    /// <summary>
    /// Implements the Log interface.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        string SoftwareName { set; get; }
        /// <summary>
        /// Get or set the log title.
        /// </summary>
        string LogTitle { set; get; }
        /// <summary>
        /// Get or set the log description.
        /// </summary>
        string LogDescription { set; get; }
        /// <summary>
        /// Get or set the log date.
        /// </summary>
        IDateLog Date { set; get; }
        /// <summary>
        /// Get or set the log type.
        /// </summary>
        ILogType Type { set; get; }
    }
}
