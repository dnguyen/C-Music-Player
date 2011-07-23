using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Music
{
    public partial class Settings : Form
    {

        public Settings()
        {
            InitializeComponent();

            // Check if the the settings file exists
            if (System.IO.File.Exists("settings.mpc"))
            {
                XmlDocument settingsXml = new XmlDocument();
                settingsXml.Load("settings.mpc");

                // Load settings when the form loads
                // Load the current library folders
                XmlNodeList cFoldersNodes = settingsXml.GetElementsByTagName("folder");
                foreach (XmlNode node in cFoldersNodes)
                {
                    // Only add folders that exist
                    if (Directory.Exists(node.InnerText))
                        listCurrentFolders.Items.Add(node.InnerText);
                    else
                        Console.WriteLine("Invalid directory: " + node.InnerText);
                }
            }
            else
            {
                MessageBox.Show("Settings file was not found. Creating one now.", "Error", MessageBoxButtons.OK);
            }
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            newFolderBrowser.ShowDialog();
            string selectedPath = newFolderBrowser.SelectedPath;
            if (Directory.Exists(selectedPath))
            {
                listCurrentFolders.Items.Add(selectedPath);

                // Write new folder to XML settings file
                try
                {
                    XmlDocument settingsXml = new XmlDocument();
                    settingsXml.Load("settings.mpc");

                    XmlNode root = settingsXml.DocumentElement;

                    XmlElement libraryelem = settingsXml.CreateElement("folder");
                    libraryelem.InnerText = selectedPath;

                    root.InsertAfter(libraryelem, root.FirstChild);

                    settingsXml.Save("settings.mpc");

                }
                catch (XmlException xe)
                {
                    Console.WriteLine(xe.ToString());
                }
            }
            else
            {
                MessageBox.Show("Invalid folder path", "Error", MessageBoxButtons.OK);
            }
        }
    }
}
