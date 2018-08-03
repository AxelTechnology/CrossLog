using Cross_Log_Library.Forms;
using System;
using System.Windows.Forms;

namespace Cross_Log
{
    public partial class FrmMain : Form
    {
        #region Properties
        private Controller controller;
        public Controller ControllerInstance
        {
            get
            {
                return controller;
            }
        }
        #endregion

        #region Constructor
        public FrmMain()
        {
            
            InitializeComponent();
            
        }
        #endregion

        #region Privates
        private void Form1_Load(object sender, EventArgs e)
        {
            controller = new Controller(this);
            this.Text = "Cross Log";
            controller.AddEditor();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblCount.Text != "10")
            {
                controller.AddEditor();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            controller.ChangeScrollPosition();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            if (controller.CheckFilters())
            {
                foreach (PanelActive panel in tableLayoutPanel1.Controls)
                {
                    //var tmp = controller.Align();
                    controller.Draw();
                }
            }
            else
            {
                MessageBox.Show("Open almost a file to apply the filters.\n\nClick on the \"?\" button on the top bar to get help.", "No file to refresh");
            }

        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpForm customAlert = new HelpForm();
            customAlert.ShowDialog();
        }

        private void btnPreferences_Click(object sender, EventArgs e)
        {
            ViewOptions vo = new ViewOptions(ControllerInstance);
            vo.Show();
        }
        #endregion

        
    }
}
