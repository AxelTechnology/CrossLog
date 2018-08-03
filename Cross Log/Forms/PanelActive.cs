using Cross_Log_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Cross_Log
{
    /// <summary> 
    /// Implement the Panel Active class.
    /// </summary>
    public partial class PanelActive : UserControl, IPanelActive
    {
        #region Properties
        private int totalLines;
        public int TotalLines
        {
            set
            {
                totalLines = value;
            }
            get
            {
                return totalLines;
            }
        }
        private int actualLine;
        public int ActualLine
        {
            set
            {
                actualLine = value;
            }
            get
            {
                return actualLine;
            }
        }
        private Boolean completed;
        public Boolean Completed
        {
            set
            {
                completed = value;
            }
            get
            {
                return completed;
            }
        }
        #endregion

        #region Attributes
        /// <summary> 
        /// The controller class.
        /// </summary>
        private Controller mController;
        /// <summary> 
        /// Used to synchronize the selection between panels.
        /// </summary>
        private List<int> mPositions;
        #endregion

        #region Constructor
        public PanelActive(Controller controller)
        {
            this.mController = controller;
            InitializeComponent();
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            mPositions = new List<int>();
            mController.SetPanelFont(this);
        }
        #endregion

        #region Privates
        /// <summary> 
        /// Calld when the user press the ... button.
        /// </summary>
        private void btnBrowse_Click(object sender, EventArgs e)
        {

            string fileName = OpenFileBrowser();
            if (fileName != null)
            {
                if (label2.Text != "Path")
                {
                    mController.RemoveList(this);
                }
                label2.Text = fileName;
                
                mController.Parse(this);
                //var tmp = mController.Align();
                mController.Draw();

            }

        }
        /// <summary> 
        /// Called when the user want to select a file using the bottom button
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            mController.RemoveEditor(this);
            mController.RemoveList(this);
        }
        /// <summary> 
        /// open the Windows File Browser.
        /// </summary>
        private string OpenFileBrowser()
        {
            string fileName = null;

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (label2.Text != "")
                {
                    openFileDialog1.InitialDirectory = label2.Text;
                }
                else
                {
                    openFileDialog1.InitialDirectory = "c:\\";
                }
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                }
            }
            return fileName;
        }
        /// <summary> 
        /// Called when the user select a box.
        /// </summary>
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            mController.ChangeSelection(this);
        }
        /// <summary> 
        /// Called when the user select arrow down button.
        /// </summary>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (mPositions.Count == 0)
            {
                mPositions = mController.SearchInEditor(this, mPositions);
            }
            int tmp = 0;
            if (mController.Selected > 0)
            {
                tmp = mController.SwitchSearchIndex(mPositions, mController.Selected);
            }
            else tmp = mController.SwitchSearchIndex(mPositions, mPositions[mPositions.Count - 1]);
            clearSelection();
            ListView1.Focus();

            ListView1.Items[tmp].Selected = true;
            Label3.Text = ((mPositions.IndexOf(tmp) + 1).ToString() + " of " + mPositions.Count.ToString());
        }
        /// <summary> 
        /// Called when the user select the close button.
        /// </summary>
        private void clearSelection()
        {
            foreach (PanelActive panel in mController.FormClass.TableLayoutPanel1.Controls)
            {
                panel.ListView1.SelectedItems.Clear();
            }
        }
        /// <summary> 
        /// Called when the user select the path label.
        /// </summary>
        private void path_click(object sender, EventArgs e)
        {
            try
            {
                var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Process.Start(label2.Text);
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
        /// <summary> 
        /// Called when the mouse pass over the label.
        /// </summary>
        private void label_paint(object sender, MouseEventArgs e)
        {
            label2.ForeColor = Color.Blue;
            label2.Font = new Font(label2.Font.Name, label1.Font.SizeInPoints, FontStyle.Underline);
        }
        /// <summary> 
        /// Called when the mouse leave the label.
        /// </summary>
        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
            label2.Font = new Font(label2.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular);
        }
        /// <summary> 
        /// Called when the panel is resized.
        /// </summary>
        private void PanelActive_Resize(object sender, EventArgs e)
        {
            if (this.Width < 480)
            {
                groupBox1.Visible = false;
                txtSearch.Visible = false;
                btnNext.Visible = false;
                lblResults.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
            }
            if (this.Width >= 480)
            {
                groupBox1.Visible = true;
                txtSearch.Visible = true;
                btnNext.Visible = true;
                lblResults.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
            }
        }
        /// <summary> 
        /// Start the text search when user press enter.
        /// </summary>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                mPositions = mController.SearchInEditor(this, mPositions);
            }
        }
        #endregion

        #region Publics
        public Int32 GetVerticalScrollbarWidth()
        {
            return GetSystemMetrics(SystemMetric.SM_CXVSCROLL);
        }
        public Int32 GetHorizontalScrollbarHeight()
        {
            return GetSystemMetrics(SystemMetric.SM_CYHSCROLL);
        }
        public enum SystemMetric
        {
            SM_CXVSCROLL = 2,
            SM_CYVSCROLL = 20,
            SM_CYHSCROLL = 3,
        }
        #endregion

        #region Statics
        [DllImport("user32.dll")]
        public static extern Int32 GetSystemMetrics(SystemMetric smIndex);


        #endregion
    }
}
