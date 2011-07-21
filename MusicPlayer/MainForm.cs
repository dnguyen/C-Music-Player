using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Music
{
    public partial class frmMain : Form
    {
        public static List<string> currentFolders;
        Settings settingsForm;

        public frmMain()
        {
            InitializeComponent();
            currentFolders = new List<string>();

            XmlDocument settingsXml = new XmlDocument();
            settingsXml.Load("settings.xml");

            // Load settings when the form loads
            // Load the current library folders
            XmlNodeList cFoldersNodes = settingsXml.GetElementsByTagName("folder");
            foreach (XmlNode node in cFoldersNodes)
            {
                currentFolders.Add(node.InnerText);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settingsForm = new Settings();
            settingsForm.Show();
        }

    }
}
