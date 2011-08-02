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

    public partial class SettingsForm : Form
    {
        bool settingsChanged = false;

        public delegate void SettingsChangedHandler(object sender, EventArgs data);
        public event SettingsChangedHandler SettingsChanged;


        public SettingsForm()
        {
            InitializeComponent();

            settingsChanged = false;

            // Check if the the settings file exists
            if (System.IO.File.Exists("settings.mpc"))
            {
                XmlDocument settingsXml = new XmlDocument();
                settingsXml.Load("settings.mpc");

                // Load settings when the form loads
                // Load the current library folders
                XmlNodeList cFoldersNodes = settingsXml.GetElementsByTagName("folder");

                // Check for Last.FM scrobbling
                XmlNode scrobblingNode = settingsXml.GetElementsByTagName("scrobble")[0];
                XmlNodeList scrobblingChildren = scrobblingNode.ChildNodes;

                foreach (XmlNode node in scrobblingChildren)
                {
                    if (node.Name == "enabled")
                    {
                        if (node.InnerText == "true")
                        {
                            chkEnableScrobbling.Checked = true;
                            txtUserName.ReadOnly = false;
                            txtPassword.ReadOnly = false;
                        }
                        else
                        {
                            chkEnableScrobbling.Checked = false;
                            txtUserName.ReadOnly = true;
                            txtPassword.ReadOnly = true;
                        }
                    }
                    if (node.Name == "username")
                        txtUserName.Text = node.InnerText;
                    else if (node.Name == "password")
                        txtPassword.Text = node.InnerText;
                }

                XmlNode MSNNode = settingsXml.GetElementsByTagName("msnenabled")[0];
                if (MSNNode.InnerText == "true")
                {
                    chkMSN.Checked = true;
                }
                else
                {
                    chkMSN.Checked = false;
                }

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
                    settingsChanged = true;

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

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (settingsChanged) // Do not want to reload the main form if we don't need to.
            {

                try
                {
                    // Save xml settings
                    XmlDocument settingsXml = new XmlDocument();
                    settingsXml.Load("settings.mpc");

                    XmlNode sNode = settingsXml.GetElementsByTagName("enabled")[0];

                    if (chkEnableScrobbling.Checked)
                        sNode.InnerText = "true";
                    else
                        sNode.InnerText = "false";

                    XmlNode userNode = settingsXml.GetElementsByTagName("username")[0];
                    XmlNode pwNode = settingsXml.GetElementsByTagName("password")[0];

                    userNode.InnerText = txtUserName.Text;
                    pwNode.InnerText = txtPassword.Text;

                    XmlNode msnNode = settingsXml.GetElementsByTagName("msnenabled")[0];
                    if (chkMSN.Checked)
                        msnNode.InnerText = "true";
                    else
                        msnNode.InnerText = "false";

                    settingsXml.Save("settings.mpc");
                }
                catch (XmlException xe)
                {
                    Console.WriteLine(xe.ToString());
                }

                // Trigger main form event
                if (SettingsChanged != null)
                {
                    SettingsChanged(this, e);
                }

            }
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            // Check if a folder was selected from the list
            if (listCurrentFolders.SelectedItems.Count > 0)
            {
                try
                {
                    XmlDocument settingsXml = new XmlDocument();
                    settingsXml.Load("settings.mpc");
                    XmlNode rootNode = settingsXml.DocumentElement;

                    XmlNodeList folders = settingsXml.GetElementsByTagName("folder");
                    XmlNode nodeToBeRemoved = null;

                    foreach (XmlNode node in folders)
                    {
                        if (node.InnerText == listCurrentFolders.SelectedItems[0].Text)
                        {
                            nodeToBeRemoved = node;
                            listCurrentFolders.Items.RemoveAt(listCurrentFolders.SelectedItems[0].Index);
                            break;
                        }
                    }

                    rootNode.RemoveChild(nodeToBeRemoved);
                    settingsXml.Save("settings.mpc");

                    settingsChanged = true;
                }
                catch (XmlException xe)
                {
                    Console.WriteLine(xe.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please select a folder to delete", "Error", MessageBoxButtons.OK);
            }
        }

        private void chkEnableScrobbling_CheckedChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
            if (chkEnableScrobbling.Checked)
            {
                txtUserName.ReadOnly = false;
                txtPassword.ReadOnly = false;
            }
            else
            {
                txtUserName.ReadOnly = true;
                txtPassword.ReadOnly = true;
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }

        private void chkMSN_CheckedChanged(object sender, EventArgs e)
        {
            settingsChanged = true;
        }
    }

    /*public class SettingsEventArgs : EventArgs
    {
        public List<string> addedFolders { get; internal set; }
        public List<string> removedFolders { get; internal set; }

        public SettingsEventArgs(List<string> addedFolders, List<string> removedFolders)
        {
            this.addedFolders = addedFolders;
            this.removedFolders = removedFolders;
        }
    }*/
}
