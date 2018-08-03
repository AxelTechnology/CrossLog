using Cross_Log_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cross_Log_Library.Classes
{
    /// <summary>
    /// Implements the Log type class.
    /// </summary>
    public class LogType : ILogType
    {
        #region Properties
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private string type = String.Empty;
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        #endregion

        #region Constructor
        public LogType(string Type)
        {
            this.Type = Type;
        }
        #endregion
    }
}
