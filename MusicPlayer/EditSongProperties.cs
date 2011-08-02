using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Music
{
    public partial class frmEditSong : Form
    {
        private Song song;

        public frmEditSong(Song song_)
        {
            InitializeComponent();
            song = song_;

            lblCurrentlyEditing.Text = song.Title + " - " + song.Artist;

            txtTitle.Text = song.Title;
            txtArtist.Text = song.Artist;
            txtAlbum.Text = song.Album;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }


    }
}
