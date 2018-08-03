using Cross_Log_Library.Interfaces;
using System;
using System.Collections.Generic;

namespace Cross_Log_Library.Classes
{
    public class LogList : ILogList, ICloneable
    {
        /// <summary>
        /// Get or set the list of the logs of a panel.
        /// </summary>
        private List<Log> listLog;
        public List<Log> ListLog
        {
            get
            {
                return listLog;
            }
            set
            {
                listLog = value;
            }
        }
        /// <summary>
        /// Get or set the panel which contains the logs.
        /// </summary>
        private IPanelActive panel;
        public IPanelActive Panel
        {
            get
            {
                return panel;
            }
            set
            {
                panel = value;
            }
        }
        /// <summary> 
        /// Add all the logs to their rispective panels.
        /// </summary>
        /// <param name="panel">The panel which contains the logs.</param>
        /// <param name="Listlog">The list of the logs of a panel.</param>
        public LogList(IPanelActive panel, List<Log> ListLog)
        {
            this.Panel = panel;
            this.ListLog = ListLog;
        }

        public object Clone()
        {
            // Create a new list with cloned objects
            var cloneList = new List<Log>();
            foreach (Log log in ListLog)
            {
                cloneList.Add((Log)log.Clone());
            }

            return new LogList(Panel, cloneList);
        }
    }
}
