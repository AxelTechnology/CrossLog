using Cross_Log_Library.Interfaces;
using System;
using System.Drawing;

namespace Cross_Log_Library.Classes
{
    /// <summary>
    /// Implements the Log class.
    /// </summary>
    [Serializable()]
    public class Log : ILog, ICloneable
    {
        #region Properties
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private string softwareName = String.Empty;
        public string SoftwareName
        {
            get
            {
                return softwareName;
            }
            set
            {
                softwareName = value;
            }
        }
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private string logTitle = String.Empty;
        public string LogTitle
        {
            get
            {
                return logTitle;
            }
            set
            {
                logTitle = value;
            }
        }
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private string logDescription = String.Empty;
        public string LogDescription
        {
            get
            {
                return logDescription;
            }
            set
            {
                logDescription = value;
            }
        }
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private IDateLog date = null;
        public IDateLog Date
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
        /// Get or set the log software name.
        /// </summary>
        private ILogType type = null;
        public ILogType Type
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
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private Color backColor = Color.White;
        public Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                backColor = value;
            }
        }
        /// <summary>
        /// Get or set the log software name.
        /// </summary>
        private Color foreColor = Color.Black;
        public Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
            }
        }
        #endregion

        #region Constructor
        public Log(string softwareName, string logTitle, string logDescription, DateLog date, LogType type)
        {
            SoftwareName = softwareName;
            LogTitle = logTitle;
            LogDescription = logDescription;
            Date = date;
            Type = type;
            foreColor = Color.Black;
            backColor = Color.White;
        }
        public Log(string softwareName, string logTitle, string logDescription, DateLog date, LogType type, Color backColor, Color foreColor)
        {
            SoftwareName = softwareName;
            LogTitle = logTitle;
            LogDescription = logDescription;
            Date = date;
            Type = type;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }

        public object Clone()
        {
            return new Log((string)SoftwareName.Clone(), (string)LogTitle.Clone(), (string)LogDescription.Clone(), (DateLog)Date, (LogType)Type, backColor, foreColor);
        }
        #endregion
    }
}
