using Cross_Log_Library.Classes;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Cross_Log_Library.Interfaces
{
    public interface ILogList
    {
        /// <summary>
        /// Get or set the list of the logs of a panel.
        /// </summary>
        List<Log> ListLog { get; set; }
        /// <summary>
        /// Get or set the panel which contains the logs.
        /// </summary>
        IPanelActive Panel { get; set; }
    }
}
