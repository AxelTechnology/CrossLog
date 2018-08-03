using Cross_Log_Library.Forms;
using System;
using System.Windows.Forms;
using System.Xml;

namespace Cross_Log
{
    public partial class ViewOptions : Form
    {
        #region Attributes
        /// <summary> 
        /// The controller class.
        /// </summary>
        private Controller controller;
        #endregion

        #region Constructor
        public ViewOptions(Controller controller)
        {
            this.controller = controller;
            InitializeComponent();
        }
        #endregion

        #region Privates
        /// <summary> 
        /// Calld when the user press the button1.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {

            var xml = new XmlDocument();
            var AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            xml.Load(AppDataPath + "\\Axel Technology\\Cross Log\\preferences.xml");

            var Nodes = xml.DocumentElement.ChildNodes;
            var FontNode = Nodes[0];
            FontNode["name"].InnerXml = txtFontName.Text;
            FontNode["size"].InnerXml = txtFontSize.Text;

            xml.Save(AppDataPath + "\\Axel Technology\\Cross Log\\preferences.xml");

            controller.SetAllPanelFont();

            this.Close();
        }
        /// <summary> 
        /// Calld when the user press the button2.
        /// </summary>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpForm customAlert = new HelpForm();
            customAlert.ShowDialog();
        }
        #endregion
    }
}
