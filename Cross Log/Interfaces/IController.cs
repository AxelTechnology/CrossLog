using Cross_Log_Library.Classes;
using Cross_Log_Library.Parsers;
using System.Collections.Generic;

namespace Cross_Log.Interfaces
{
    /// <summary>
    /// Implements the Controller interface.
    /// </summary>
    interface IController
    {
        /// <summary>
        /// Get or set the log list.
        /// </summary>
        List<LogList> LogList { set; get; }
        /// <summary>
        /// Get or set the parser.
        /// </summary>
        Parser ParserClass { set; get; }
        /// <summary>
        /// Get or set the drawer.
        /// </summary>
        Drawer DrawerClass { set; get; }
        /// <summary>
        /// Get or set the Form.
        /// </summary>
        FrmMain FormClass { set; get; }

        /// <summary>
        /// Add a new editor in the UI.
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        void AddEditor();
        /// <summary> 
        /// Remove the param from the main form.
        /// </summary>
        /// <param name="panel">The panel to remove.</param>
        void RemoveEditor(PanelActive panel);
        /// <summary> 
        /// Execute the parsing process, with the drawing process.
        /// </summary>
        /// <param name="panel">Where to put the parsed content.</param>
        void Parse(PanelActive panel);
        /// <summary> 
        /// Check the setted filters.
        /// </summary>
        bool CheckFilters();
        /// <summary> 
        /// Remove or add the Function Call column.
        /// </summary>
        /// <param name="status">The checkbox status.</param>
        void FunctionCallChangedView(bool status);
        /// <summary> 
        /// Change the editors scrollbar position according to the current position of the universal scroll bar.
        /// </summary>
        void ChangeScrollPosition();
        /// <summary> 
        /// Search for the inserted text in the entire panel.
        /// </summary>
        /// <param name="panel">The panel in which to search.</param>
        List<int> SearchInEditor(PanelActive panel, List<int> lista);
    }
}
