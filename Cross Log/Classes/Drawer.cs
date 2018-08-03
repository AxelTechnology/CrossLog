using Cross_Log.Interfaces;
using Cross_Log_Library.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cross_Log
{
    public class Drawer : IDrawer
    {

        #region Properties
        /// <summary>
        /// Get or set the main form.
        /// </summary>
        private FrmMain form = null;
        public FrmMain Form
        {
            get
            {
                return form;
            }
            set
            {
                form = value;
            }
        }
        #endregion

        #region Constructor
        public Drawer(FrmMain form)
        {
            this.form = form;
        }
        #endregion

        #region Public
        /// <summary> 
        /// Add an editor to the main form.
        /// </summary>
        public void AddEditor()
        {


            form.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            form.TableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            form.TableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;

            PanelActive panel = new PanelActive(form.ControllerInstance);
            form.TableLayoutPanel1.Controls.Add(panel);
            panel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            form.Label4.Text = (Int32.Parse(form.Label4.Text) + 1).ToString();
            panel.Label1.Text = "Viewer " + (Int32.Parse(form.Label4.Text)).ToString();
            panel.ListView1.View = View.Details;
            panel.ListView1.AllowColumnReorder = true;
            panel.ListView1.Columns.Add("Time", 90, HorizontalAlignment.Left);
            panel.ListView1.Columns.Add("Mode", 70, HorizontalAlignment.Left);
            panel.ListView1.Columns.Add("Description", 500, HorizontalAlignment.Left);
            if (true)
            {
                panel.ListView1.Columns.Add("Function Call", 200, HorizontalAlignment.Left);
            }

        }
        /// <summary> 
        /// Remove the panel from the main form.
        /// </summary>
        /// <param name="panel">The panel to remove.</param>
        public void RemoveEditor(PanelActive panel)
        {
            form.TableLayoutPanel1.Controls.Remove(panel);
            form.Label4.Text = (Int32.Parse(form.Label4.Text) - 1).ToString();
        }
        /// <summary> 
        /// Add all the logs to their rispective panels.
        /// </summary>
        /// <param name="logList">The list containing all the logs.</param>
        public void AddToEditor(List<LogList> logList, int start, int end)
        {
            int i = 0;
            foreach (LogList lista in logList)
            {

                lista.Panel.ProgressBar1.Visible = true;
                if (lista.ListLog.Count > 0)
                {
                    lista.Panel.TotalLines = lista.ListLog.Count;

                    /***** Get the software name from the file path *****/
                    int n = 0;
                    for (n = 0; lista.ListLog[n].SoftwareName == ""; n++)
                    {

                    }
                    lista.Panel.Label1.Text = lista.ListLog[n].SoftwareName;
                    /******************************************************/

                    /***** Remove ALL the items from the actual list *****/
                    lista.Panel.ListView1.Items.Clear();
                    /******************************************************/

                    /***** Creating the list to add *****/
                    int x = 0;
                    ListViewGroup list = new ListViewGroup();
                    foreach (Log log in lista.ListLog)
                    {
                        if (start <= x && x <= end)
                        {
                            /***** Formatting the milliseconds text *****/
                            var milliseconds = "0";
                            if (log.Date.Date.Millisecond <= 0)
                                milliseconds = "00" + log.Date.Date.Millisecond.ToString();
                            else if (log.Date.Date.Millisecond < 100)
                                milliseconds = "0" + log.Date.Date.Millisecond.ToString();
                            else
                                milliseconds = log.Date.Date.Millisecond.ToString();
                            /******************************************************/

                            /***** Populating the new item *****/
                            ListViewItem lvi = new ListViewItem(log.Date.Date.ToString().Remove(0, 10) + "." + milliseconds);
                            lvi.SubItems.Add(log.Type.Type);
                            lvi.SubItems.Add(log.LogDescription);
                            if (log.Type.Type == "ERROR")
                            {
                                lvi.ForeColor = lista.ListLog[x].ForeColor;
                                lvi.BackColor = lista.ListLog[x].BackColor;
                            }
                            lvi.SubItems.Add(log.LogTitle);

                            // Changing the time color if the line is empty
                            if (log.LogDescription == "")
                            {
                                lvi.ForeColor = lista.ListLog[x].ForeColor;
                            }

                            if (x % 2 == 0)
                            {
                                lvi.BackColor = lista.ListLog[x].BackColor;
                            }

                            list.Items.Add(lvi);
                            /******************************************************/


                            var tmp = lista.Panel.ProgressBar1;
                            tmp.Minimum = 0;
                            tmp.Maximum = lista.ListLog.Count;
                            tmp.Step = 1;
                            tmp.PerformStep();
                            lista.Panel.ProgressBar1 = tmp;

                            
                        }
                        x++;
                    }

                    lista.Panel.ListView1.Items.AddRange(list.Items);
                    lista.Panel.ActualLine = x;
                    ListViewItem lviEnd = new ListViewItem("");
                    lviEnd.SubItems.Add(" ");
                    lviEnd.SubItems.Add("Fine del file selezionato");
                    lviEnd.BackColor = Color.Yellow;
                    lviEnd.SubItems.Add("");
                    lista.Panel.ListView1.Items.Add(lviEnd);
                    lista.Panel.ProgressBar1.Visible = false;
                    lista.Panel.ProgressBar1.Value = 0;

                    i++;
                }
                else
                {
                    ListViewItem lviEnd = new ListViewItem("");
                    lviEnd.SubItems.Add(" ");
                    lviEnd.SubItems.Add("No logs in the selected range");
                    lviEnd.BackColor = Color.Red;
                    lviEnd.ForeColor = Color.White;
                    lviEnd.SubItems.Add("");
                    lista.Panel.ListView1.Items.Add(lviEnd);
                    i++;
                }

            }
        }
        /// <summary> 
        /// Remove the column at the param from all the panels.
        /// </summary>
        /// <param name="index">The index of the column to remove.</param>
        public void RemoveColumn(int index)
        {
            foreach (PanelActive panel in Form.TableLayoutPanel1.Controls)
            {
                panel.ListView1.Columns.RemoveAt(index);
            }
        }
        /// <summary> 
        /// Add the column at the param in all the panels.
        /// </summary>
        public void AddColumn()
        {
            foreach (PanelActive panel in Form.TableLayoutPanel1.Controls)
            {
                panel.ListView1.Columns.Add("Function Call", 200, HorizontalAlignment.Left);
            }
        }
        #endregion
    }
}
