using Cross_Log_Library.Classes;
using System;
using System.Collections.Generic;

namespace Cross_Log.Interfaces
{
    interface IDrawer
    {
        /// <summary>
        /// Get or set the main form.
        /// </summary>
        FrmMain Form { set; get; }

        /// <summary> 
        /// Add an editor to the main form.
        /// </summary>
        void AddEditor();
        /// <summary> 
        /// Remove the param from the main form.
        /// </summary>
        /// <param name="panel">The panel to remove.</param>
        void RemoveEditor(PanelActive panel);
        /// <summary> 
        /// Add all the logs to their rispective panels.
        /// </summary>
        /// <param name="logList">The list containing all the logs.</param>
        void AddToEditor(List<LogList> logList, int start, int end);
        /// <summary> 
        /// Remove the column at the param from all the panels.
        /// </summary>
        /// <param name="index">The index of the column to remove.</param>
        void RemoveColumn(int index);
        /// <summary> 
        /// Add the column at the param in all the panels.
        /// </summary>
        void AddColumn();
    }
}
