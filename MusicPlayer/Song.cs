using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Music
{
    class Song
    {
        private string path;
        private string title;
        private string artist;

        /* Properties */
        public string Path { get { return path; } set { path = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Artist { get { return artist; } set { artist = value; } }

        public Song(string path_, string title_, string artist_)
        {
            path = path_;
            title = title_;
            artist = artist_;
        }
    }
}
