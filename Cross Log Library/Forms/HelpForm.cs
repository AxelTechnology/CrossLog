using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Cross_Log_Library.Forms
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();

            label16.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Axel Technology\\Cross Log\\preferences.xml";
            label16.ForeColor = Color.Blue;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Process.Start(AppDataPath + "\\Axel Technology\\Cross Log\\settings.xml");
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void btnReferences_Click(object sender, EventArgs e)
        {
            try
            {
                var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                Process.Start(AppDataPath + "\\Axel Technology\\Cross Log\\preferences.xml");
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
