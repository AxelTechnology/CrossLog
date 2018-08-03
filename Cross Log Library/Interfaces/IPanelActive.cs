using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cross_Log_Library.Interfaces
{
    public interface IPanelActive
    {
        #region Properties
        /// <summary> 
        /// The controller class.
        /// </summary>
        Label Label1 { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        Label Label2 { get; set; }
        /// <summary> 
        /// The controller class.
        /// </summary>
        TextBox TextBox1 { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        Label Label4 { get; set; }
        /// <summary> 
        /// The controller class.
        /// </summary>
        Label Label3 { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        ListView ListView1 { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        ProgressBar ProgressBar1 { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        int TotalLines { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        int ActualLine { get; set; }
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        Boolean Completed { get; set; }
        #endregion
    }
}
