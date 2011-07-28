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
using Lastfm;
using TagLib;

using System.Net;
using Lastfm.Services;
using Lastfm.Scrobbling;

namespace Music
{
    public partial class frmMain : Form
    {
        /*public List<Dictionary<string, List<Song>>> cFolders; // Holds ALL the folders of music
        // Dictionary key - folderpath, value - music files within that folder
        public Dictionary<string, List<Song>> MusicFolder; // Just ONE folder of music*/

        public List<string> currentFolders;
        public List<Song> listSongs;

        SettingsForm settingsForm;

        bool scrobblingEnabled = false;
        bool doneLoadingSong = false;

        string LFMAPI_KEY = "d79f4d7f16bc620937bb85a627e4d09f";
        string LFMAPI_SECRET = "d51b8832bf327d074f1834bde8ed17b3";

        ScrobbleManager scrobbleManager;
        Session session;

        public frmMain()
        {
            InitializeComponent();

            currentFolders = new List<string>();
            listSongs = new List<Song>();

            LoadCurrentFolders();
            LoadSongs();
            LoadSettings();
            player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(myPlayer_PlayStateChange);
            
        }

        private void listMusic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PlaySong(listSongs[listMusic.SelectedIndices[0]].Path);
            
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
            LoadSettings();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            listMusic.Width = this.Width - 40;
            listMusic.Height = this.Height - 200;
            songDuration.Width = this.Width - 122;
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


        void myPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e) {
            if (doneLoadingSong && e.newState == 3)
            {
                songDuration.Maximum = (int)player.currentMedia.duration;
                lblSongDuration.Text = player.currentMedia.durationString;

                Entry entry = new Entry(
                    listSongs[listMusic.SelectedIndices[0]].Artist,
                    listSongs[listMusic.SelectedIndices[0]].Title,
                    DateTime.Now,
                    PlaybackSource.NonPersonalizedBroadcast,
                    TimeSpan.FromSeconds(player.currentMedia.duration),
                    ScrobbleMode.Played);

                scrobbleManager.Queue(entry);

                doneLoadingSong = false;
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySong(listSongs[listMusic.SelectedIndices[0]].Path);
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
        }

        #region Form functions (NOT events)

        private void PlaySong(string path)
        {
            // Use last.fm to get the album art
            string lastFMURL = "http://ws.audioscrobbler.com/2.0/?method=album.getinfo&api_key=d79f4d7f16bc620937bb85a627e4d09f&artist=" + listSongs[listMusic.SelectedIndices[0]].Artist + "&album=" + listSongs[listMusic.SelectedIndices[0]].Album;
            try
            {
                XmlDocument lastFMXML = new XmlDocument();
                lastFMXML.Load(lastFMURL);

                XmlNodeList ImageNodes = lastFMXML.GetElementsByTagName("image");
                foreach (XmlNode node in ImageNodes)
                {
                    if (node.Attributes["size"].InnerText == "medium")
                    {
                        imgAlbumArt.ImageLocation = node.InnerText;
                        ///Console.WriteLine(node.Attributes["size"].InnerText);
                    }

                }
                
            }
            catch (XmlException xe)
            {
                Console.WriteLine(xe.ToString());
            }

            timerSongDuration.Enabled = true;
            doneLoadingSong = true;
            songDuration.Value = 0;
            player.URL = path;
            lblCurrentSong.Text = listSongs[listMusic.SelectedIndices[0]].Title + " - " + listSongs[listMusic.SelectedIndices[0]].Artist;
            player.Ctlcontrols.play();
        }

        private void StopSong()
        {
            player.Ctlcontrols.stop();
            timerSongDuration.Stop();
            songDuration.Value = 0;
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

        private void LoadSettings()
        {
            if (System.IO.File.Exists("settings.mpc"))
            {
                try
                {
                    XmlDocument settingsXml = new XmlDocument();
                    settingsXml.Load("settings.mpc");

                    // Check for Last.FM scrobbling
                    XmlNode scrobblingNode = settingsXml.GetElementsByTagName("scrobble")[0];
                    XmlNodeList scrobblingChildren = scrobblingNode.ChildNodes;
                    foreach (XmlNode node in scrobblingChildren)
                    {
                        if (node.Name == "enabled")
                        {
                            if (node.InnerText == "true")
                                scrobblingEnabled = true;
                            else
                                scrobblingEnabled = false;
                        }
                    }

                    // If scrobbling is enabled, authentican with last.fm
                    if (scrobblingEnabled)
                    {
                        // Grab username and password for last.fm auth
                        session = new Session(LFMAPI_KEY, LFMAPI_SECRET);
                        string md5Password = Lastfm.Utilities.md5(settingsXml.GetElementsByTagName("password")[0].InnerText);

                        try
                        {
                            session.Authenticate(settingsXml.GetElementsByTagName("username")[0].InnerText, md5Password);
                        } catch {
                            Console.WriteLine("Failed to authenticate");
                        }

                        if (session.Authenticated)
                        {
                            Connection connection = new Connection("mpc", "1.0", settingsXml.GetElementsByTagName("username")[0].InnerText, session);
                            scrobbleManager = new ScrobbleManager(connection, "../cache/");
                            
                            Console.WriteLine("Authenticated!");
                        }
                    }
                }
                catch (XmlException xe) {
                    Console.WriteLine(xe.ToString());
                }
            }
        }

        #endregion
    }
}
