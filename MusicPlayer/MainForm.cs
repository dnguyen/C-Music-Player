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
using TagLib;

namespace Music
{
    public partial class frmMain : Form
    {
        public static List<string> currentFolders;
        public List<Song> listSongs;

        Settings settingsForm;

        public frmMain()
        {
            InitializeComponent();
            currentFolders = new List<string>();
            listSongs = new List<Song>();
            // Check if the the settings file exists
            if (System.IO.File.Exists("settings.xml"))
            {
                XmlDocument settingsXml = new XmlDocument();
                settingsXml.Load("settings.xml");

                // Load settings when the form loads
                // Load the current library folders
                XmlNodeList cFoldersNodes = settingsXml.GetElementsByTagName("folder");
                foreach (XmlNode node in cFoldersNodes)
                {
                    // Only add folders that exist
                    if (Directory.Exists(node.InnerText))
                        currentFolders.Add(node.InnerText);
                    else
                        Console.WriteLine("Invalid directory: " + node.InnerText);
                }
            }
            else
            {
                MessageBox.Show("Settings file was not found. Creating one now.", "Error", MessageBoxButtons.OK); 
            }

            // Fill in the listview with all music files after done loading the current folders
            if (currentFolders.Count > 0)
            {
                int count = 0;
                foreach (string fPath in currentFolders)
                {
                    string[] musicFiles = Directory.GetFiles(fPath);
                    
                    foreach (string musicFile in musicFiles)
                    {
                        TagLib.File mFile = TagLib.File.Create(musicFile);
                        Song songFile = new Song(musicFile, mFile.Tag.FirstAlbumArtist, mFile.Tag.Title);

                        listSongs.Add(songFile);
                        listMusic.Items.Add(songFile.Artist);
                        listMusic.Items[count].SubItems.Add(songFile.Title);
                        listMusic.Items[count].SubItems.Add(songFile.Path);

                        count++;
                    }

                }
            }
            else
            {
                Console.WriteLine("Current folders count <= 0");
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            settingsForm = new Settings();
            settingsForm.Show();
        }

        private void listMusic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            player.URL = listSongs[listMusic.SelectedIndices[0]].Path;
        }

    }
}
