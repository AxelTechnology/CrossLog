using Cross_Log.Interfaces;
using Cross_Log_Library.Classes;
using Cross_Log_Library.Parsers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
namespace Cross_Log
{
    /// <summary>
    /// Implements the Log interface.
    /// </summary>
    public class Controller : IController
    {
        #region Properties
        /// <summary>
        /// Get or set aligner class.
        /// </summary>
        private Aligner alignerClass;
        public Aligner AlignerClass
        {
            get
            {
                return alignerClass;
            }
            set
            {
                alignerClass = value;
            }
        }
        /// <summary>
        /// Get or set the log list.
        /// </summary>
        private List<LogList> logList = new List<LogList>();
        public List<LogList> LogList {
            get
            {
                return logList;
            }
            set
            {
                LogList = value;
            }
        }
        /// <summary>
        /// Get or set the parser class.
        /// </summary>
        private Parser parserClass = null;
        public Parser ParserClass {
            get
            {
                return parserClass;
            }
            set
            {
                parserClass = value;
            }
        }
        /// <summary>
        /// Get or set the drawer class.
        /// </summary>
        private Drawer drawerClass = null;
        public Drawer DrawerClass {
            get
            {
                return drawerClass;
            }
            set
            {
                drawerClass = value;
            }
        }
        /// <summary>
        /// Get or set the form class.
        /// </summary>
        private FrmMain formClass = null;
        public FrmMain FormClass
        {
            get
            {
                return formClass;
            }
            set
            {
                formClass = value;
            }
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Get or set the log list.
        /// </summary>
        private int selected;
        public int Selected
        {
            get
            {
                return selected;
            }
        }
        #endregion

        #region Constructor
        public Controller(FrmMain form)
        {
            formClass = form;
            drawerClass = new Drawer(form);
            AlignerClass = new Aligner();
            selected = 0;
            
        }
        #endregion

        #region Publics
        /// <summary> 
        /// Add an editor in the main form.
        /// </summary>
        public void AddEditor()
        {
            drawerClass.AddEditor();
        }
        /// <summary> 
        /// Remove the param from the main form.
        /// </summary>
        /// <param name="panel">The panel to remove.</param>
        public void RemoveEditor(PanelActive panel)
        {
            drawerClass.RemoveEditor(panel);
        }
        /// <summary> 
        /// Remove the param from the main form.
        /// </summary>
        /// <param name="panel">The panel to remove.</param>
        public void RemoveList(PanelActive panel)
        {
            
            foreach (LogList list in LogList)
            {
                if (list.Panel.Equals(panel))
                {
                    LogList.Remove(list);
                    break;
                }
            }
            //var tmp = Align();
            Draw();

        }
        /// <summary> 
        /// Remove all the lists.
        /// </summary>
        public void ResetAllLists()
        {
            LogList.Clear();
        }
        /// <summary> 
        /// Execute the parsing process, with the drawing process.
        /// </summary>
        /// <param name="panel">Where to put the parsed content.</param>
        public void Parse(PanelActive panel)
        {
            var FileName = Path.GetFileName(panel.Label2.Text);
            var ParsingMode = GetParsingMode(FileName, panel.Label2.Text);
            if (ParsingMode == 1)
            {
                ParserClass = new ParserMode1();
            }
            else if (ParsingMode == 2)
            {
                ParserClass = new ParserMode2();
            }
            else if (ParsingMode == 3)
            {
                ParserClass = new ParserMode3();
            }
            else if (ParsingMode == 4)
            {
                ParserClass = new ParserMode4();
            }
            else
            {
                MessageBox.Show("Cross Log cannot read this file.\n\nTo get help, click the \"?\" button on the top menu.", "Invalid parsing mode");
                return;
            }

            var taskResult = new List<Log>();
            taskResult = ParserClass.Parse(panel.Label2.Text);

            var tmp = ParserClass.GetDateFromFileName(panel.Label2.Text);
            if (tmp.Year != 1)
            {
                panel.Label4.Text = tmp.Day + " - " + tmp.Month + " - " + tmp.Year;
            }
            else
            {
                panel.Label4.Text = "Cannot load date";
            }

            logList.Add(new LogList(panel, taskResult));

            return;

        }
        /// <summary> 
        /// Align the logs
        /// </summary>
        public List<LogList> Align()
        {
            // Cloning the list
            var tmp = new List<LogList>();
            foreach (LogList logListToInsert in logList)
            {
                tmp.Add((LogList)logListToInsert.Clone());
            }
            return AlignerClass.AllignParsed(tmp);
        }
        /// <summary> 
        /// Draw all the lists.
        /// </summary>
        public void Draw()
        {
            var tmp = Align();
            if (!CheckFilters())
            {
                drawerClass.AddToEditor(tmp, 0, int.MaxValue);
            }
            else
            {
                DateTime date = new DateTime(1, 1, 1, Int32.Parse(formClass.ComboBox1.Text), Int32.Parse(formClass.ComboBox2.Text), 0);
                drawerClass.AddToEditor(tmp, getFirstLogPosition(date, tmp), getLastLogPosition(date.AddMinutes(Int32.Parse(formClass.ComboBox3.Text)), tmp));
            }
        }
        /// <summary> 
        /// Check the setted filters.
        /// </summary>
        public bool CheckFilters()
        {
            return ((formClass.ComboBox1.Text != "") && (formClass.ComboBox2.Text != "") && (formClass.ComboBox3.Text != ""));
        }
        /// <summary> 
        /// Remove or add the Function Call column.
        /// </summary>
        /// <param name="status">The checkbox status.</param>
        public void FunctionCallChangedView(bool status)
        {
            if (!status)
            {
                drawerClass.RemoveColumn(3);
            }
            else
            { 
                drawerClass.AddColumn();
            }
        }
        /// <summary> 
        /// Change the editors scrollbar position according to the current position of the universal scroll bar.
        /// </summary>
        public void ChangeScrollPosition()
        {
            foreach (PanelActive panel in FormClass.TableLayoutPanel1.Controls)
            {
                int ScrollPercentage = FormClass.VScrollBar1.Value;
                int tmp = panel.ListView1.Items.Count / 90;
                if ( panel.ListView1.Items.Count > ScrollPercentage * tmp)
                {
                    //var ScrollPercentage = FormClass.VScrollBar1.Value;
                    //var ll = FormClass.VScrollBar1.Size;
                    //var lm = panel.GetVerticalScrollbarWidth();
                    //var ScrollEffective = ScrollPercentage * (panel.ListView1.Items.Count / 100);
                    //SetScrollPos(panel.ListView1.Handle, 1, ScrollEffective, true);
                    //panel.ListView1.Items[ScrollEffective].EnsureVisible();
                    
                    panel.ListView1.TopItem = panel.ListView1.Items[ScrollPercentage * tmp];
                }
                
            }
        }
        /// <summary> 
        /// Change universal scroll bar position according to the selected line.
        /// </summary>
        public void ChangeMainScrollPosition(int line, PanelActive panel)
        {
            FormClass.VScrollBar1.Value = (line * 90) / panel.ListView1.Items.Count;
        }
        /// <summary> 
        /// Search for the inserted text in the entire panel.
        /// </summary>
        /// <param name="panel">The panel in which to search.</param>
        public List<int> SearchInEditor(PanelActive panel, List<int> lista)
        {
            if (panel.TextBox1.Text == "")
            {
                panel.Label3.Text = "";
                return new List<int>();
            }
            if (panel.ListView1.Items.Count > 0)
            {
                panel.Label3.Text = "0";
                foreach (System.Windows.Forms.ListViewItem item in panel.ListView1.Items)
                {
                    if (item.Text.IndexOf(panel.TextBox1.Text, StringComparison.OrdinalIgnoreCase)>=0)
                    {
                        lista.Add(item.Index);
                        panel.Label3.Visible = true;
                        panel.Label3.Text = (Int32.Parse(panel.Label3.Text) + 1).ToString();
                    }
                    int i = 0;
                    foreach (System.Windows.Forms.ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        if (subItem.Text.IndexOf(panel.TextBox1.Text, StringComparison.OrdinalIgnoreCase)>=0)
                        {
                            lista.Add(item.Index);
                            panel.Label3.Visible = true;
                            panel.Label3.Text = (Int32.Parse(panel.Label3.Text) + 1).ToString();
                            i++;
                        }
                    }
                }
                panel.Label3.Text = panel.Label3.Text + " matches";
            }
            return lista;
        }
        /// <summary> 
        /// Switch the index for the searching process.
        /// </summary>
        /// <param name="lista">The list that will be used.</param>
        /// <param name="linea">The actual position.</param>
        public int SwitchSearchIndex(List<int> lista, int linea)
        {
            if (lista.IndexOf(linea) == lista.Count - 1)
                return lista[0];
            return lista[lista.IndexOf(linea) + 1];
        }
        /// <summary> 
        /// Change the selected line in all the editors.
        /// </summary>
        public void ChangeSelection(PanelActive panelActive)
        {
            if (panelActive.ListView1.SelectedItems.Count <= 0)
            {
                return;
            }
            int t = 0;
            foreach (PanelActive panel in FormClass.TableLayoutPanel1.Controls)
            {
                if (panel.ListView1.Items.Count > 0)
                {
                    if (selected== LogList[t].ListLog.Count)
                    {
                        panel.ListView1.Items[selected].BackColor = Color.Yellow;
                        panel.ListView1.Items[selected].ForeColor = Color.Black;
                    }
                    else
                    {
                        panel.ListView1.Items[selected].BackColor = LogList[t].ListLog[selected].BackColor;
                        panel.ListView1.Items[selected].ForeColor = LogList[t].ListLog[selected].ForeColor;
                        LogList[t].ListLog[selected].BackColor = panel.ListView1.Items[selected].BackColor;
                        LogList[t].ListLog[selected].ForeColor = panel.ListView1.Items[selected].ForeColor;
                    }
                    
                }
                t++;
            }

            selected = panelActive.ListView1.SelectedItems[panelActive.ListView1.SelectedItems.Count - 1].Index;
            panelActive.ListView1.Items[selected].EnsureVisible();
            t = 0;
            foreach (PanelActive panel in FormClass.TableLayoutPanel1.Controls)
            {

                if (panelActive.ListView1.SelectedItems.Count > 0 && panel.ListView1.Items.Count >= selected)
                {
                    panel.ListView1.Items[selected].BackColor = Color.DodgerBlue;
                    panel.ListView1.Items[selected].ForeColor = Color.White;
                    panel.ListView1.TopItem = panel.ListView1.Items[panelActive.ListView1.Items.IndexOf(panelActive.ListView1.TopItem)];
                }
                t++;
            }
            panelActive.ListView1.SelectedItems.Clear();
            ChangeMainScrollPosition(selected, panelActive);
        }
        /// <summary> 
        /// Change the list view font due XML settings.
        /// </summary>
        public void SetPanelFont(PanelActive panel)
        {
            try
            {
                var xml = new XmlDocument();
                var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                xml.Load(AppDataPath + "\\Axel Technology\\Cross Log\\preferences.xml");

                var Nodes = xml.DocumentElement.ChildNodes;
                var FontNode = Nodes[0];
                var FontName = FontNode["name"].InnerText;
                var FontSize = Int32.Parse(FontNode["size"].InnerText);

                panel.ListView1.Font = new Font(FontName, FontSize);
            }
            catch
            {
                MessageBox.Show("Cross Log cannot read the value from the XML consfiguration file.\nThis is caused by a bad formatted string in the file.\n\nTo fix the problem click the \"?\" button on the top menu.", "Invalid XML value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary> 
        /// Change the list view font due XML settings.
        /// </summary>
        public void SetAllPanelFont()
        {
            foreach (PanelActive panel in FormClass.TableLayoutPanel1.Controls)
            {
                SetPanelFont(panel);
            }
        }
        #endregion

        #region Statics
        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll")]
        public static extern int GetScrollPos(IntPtr hwnd, int nBar);
        #endregion

        #region Privates
        /// <summary> 
        /// Get the parsing mode from XML.
        /// </summary>
        private int GetParsingMode(string fileName, string path)
        {

            var xml = new XmlDocument();
            var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xml.Load(AppDataPath + "\\Axel Technology\\Cross Log\\settings.xml");

            foreach (XmlNode node in xml.DocumentElement.ChildNodes)
            {
                Match match = Regex.Match(fileName, node["name"].InnerText, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return Int32.Parse(node["value"].InnerText);
                }
            }
            return 0;
            
        }
        /// <summary> 
        /// Get the parsing mode from XML.
        /// </summary>
        private bool IsYear(string input)
        {
            var tmp = new DateTime();
            DateTime.TryParse(input, out tmp);
            return true;
        }
        /// <summary> 
        /// Get the software name.
        /// </summary>
        private string GetSoftwareName(string line)
        {
            line = line.Replace("\t", "£");
            var tabPosition = line.IndexOf("£");
            line = line.Remove(0, tabPosition + 1);

            tabPosition = line.IndexOf("£");
            line = line.Remove(tabPosition);

            return line;
        }
        /// <summary> 
        /// Get the software name.
        /// </summary>
        private List<LogList> clear(List<LogList> list)
        {
            if (list.Count == 0)
            {
                return list;
            }
            bool del = false;
            bool last = false;
            for (int x = 0; x < list[0].ListLog.Count; x++)
            {
                if (del == true)
                {
                    last = true;
                }
                del = true;
                foreach (LogList l in list)
                {
                    if (last == true)
                    {
                        l.ListLog.RemoveAt(x);
                    }
                    if (l.ListLog[x].LogTitle != "")
                    {
                        del = false;
                    }
                }
                if (del == true)
                {
                    x--;
                }
                last = false;
            }
            return list;
        }
        /// <summary> 
        /// Get the software name.
        /// </summary>
        private int getLastLogPosition(DateTime last, List<LogList> list)
        {
            if (list.Count == 0)
            {
                return int.MaxValue;
            }
            int pos = 0;
            foreach(Log l in list[0].ListLog)
            {
                if (l.Date.Date > last)
                {
                    return pos-1;
                }
                pos++;
            }
            return list[0].ListLog.Count - 1;
        }
        /// <summary> 
        /// Get the software name.
        /// </summary>
        private int getFirstLogPosition(DateTime last, List<LogList> list)
        {
            if (list.Count == 0)
            {
                return 0;
            }
            int pos = list[0].ListLog.Count - 1;
            foreach (Log l in list[0].ListLog)
            {
                if (l.Date.Date < last)
                {
                    return pos + 1;
                }
                pos--;
            }
            return 0;
        }
        #endregion
    }
}