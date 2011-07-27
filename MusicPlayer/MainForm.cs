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
        public List<Dictionary<string, List<Song>>> cFolders; // Holds ALL the folders of music
        // Dictionary key - folderpath, value - music files within that folder
        public Dictionary<string, List<Song>> MusicFolder; // Just ONE folder of music

        public List<string> currentFolders;
        public List<Song> listSongs;

        SettingsForm settingsForm;

        bool doneLoadingSong = false;

        public frmMain()
        {
            InitializeComponent();

            currentFolders = new List<string>();
            listSongs = new List<Song>();

            LoadCurrentFolders();
            LoadSongs();

            player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(myPlayer_PlayStateChange);
            
        }

        private void listMusic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PlaySong(listSongs[listMusic.SelectedIndices[0]].Path);
            timerSongDuration.Enabled = true;
            doneLoadingSong = true;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm = new SettingsForm();
            settingsForm.SettingsChanged += new SettingsForm.SettingsChangedHandler(SettingsHaveChanged);
            settingsForm.ShowDialog();
        }

        public void SettingsHaveChanged(object sender, EventArgs e)
        {
            /*
             * TODO: Find a better way of doing this. Don't want to load the entire list again.
             * */
            listMusic.Items.Clear();
            listSongs.Clear();
            currentFolders.Clear();
            LoadCurrentFolders();
            LoadSongs();
        }



        private void frmMain_Resize(object sender, EventArgs e)
        {
            listMusic.Width = this.Width - 40;
            listMusic.Height = this.Height - 180;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            bool foundResults = false;
            List<Song> songResults = new List<Song>();
            
            foreach (Song song in listSongs)
            {

                if (song.Title.ToLower().Contains(txtSearch.Text.ToLower()) || song.Artist.ToLower().Contains(txtSearch.Text.ToLower()) || song.Album.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    songResults.Add(song);
                    foundResults = true;
                }
            }

            if (foundResults)
            {
                listSongs.Clear();
                listSongs = songResults;

                listMusic.Items.Clear();

                int count = 0;
                foreach (Song song in songResults)
                {
                    listMusic.Items.Add(song.Title);
                    listMusic.Items[count].SubItems.Add(song.Artist);
                    listMusic.Items[count].SubItems.Add(song.Album);
                    count++;
                }

                btnCloseSearch.Show();
                
            }
        }

        private void btnCloseSearch_Click(object sender, EventArgs e)
        {
            // Slowwwwwww..w..w.w.w.w.w..w......
            
            listMusic.Items.Clear();
            listSongs.Clear();
            LoadSongs();
            btnCloseSearch.Hide();
        }

        private void timerSongDuration_Tick(object sender, EventArgs e)
        {
            if (songDuration.Value < songDuration.Maximum)
                songDuration.Value++;
        }

        private void playToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PlaySong(listSongs[listMusic.SelectedIndices[0]].Path);
        }

        private void PlaySong(string path)
        {
            songDuration.Value = 0;
            player.URL = path;
            lblCurrentSong.Text = listSongs[listMusic.SelectedIndices[0]].Title + " - " + listSongs[listMusic.SelectedIndices[0]].Artist;
            player.Ctlcontrols.play();
        }

        private void LoadCurrentFolders()
        {
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
                        currentFolders.Add(node.InnerText);
                    else
                        Console.WriteLine("Invalid directory: " + node.InnerText);
                }
            }
            else
            {
                MessageBox.Show("Settings file was not found. Creating one now.", "Error", MessageBoxButtons.OK);
            }
        }

        private void LoadSongs()
        {
            // Fill in the listview with all music files after loading the current folders
            if (currentFolders.Count > 0)
            {
                int count = 0;
                foreach (string fPath in currentFolders)
                {
                    string[] musicFiles = Directory.GetFiles(fPath);

                    foreach (string musicFile in musicFiles)
                    {
                        if (Path.GetExtension(musicFile) == ".mp3" || Path.GetExtension(musicFile) == ".wmv" || Path.GetExtension(musicFile) == ".MP3")
                        {
                            TagLib.File mFile = TagLib.File.Create(musicFile);

                            Song songFile = new Song(musicFile, mFile.Tag.Title, mFile.Tag.FirstAlbumArtist, mFile.Tag.Album);

                            listSongs.Add(songFile);
                            listMusic.Items.Add(songFile.Title);
                            listMusic.Items[count].SubItems.Add(songFile.Artist);
                            listMusic.Items[count].SubItems.Add(songFile.Album);

                            count++;
                        }
                        else
                        {
                            Console.WriteLine("Error: " + Path.GetExtension(musicFile));
                            Console.WriteLine("Invalid file: " + musicFile);
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Current folders count <= 0");
            }
        }

        void myPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e) {
            if (doneLoadingSong && e.newState == 3)
            {
                songDuration.Maximum = (int)player.currentMedia.duration;
                lblSongDuration.Text = player.currentMedia.durationString;
                doneLoadingSong = false;
            }
        }
    }
}
