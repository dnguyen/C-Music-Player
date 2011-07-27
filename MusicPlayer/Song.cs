using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Music
{
    public class Song
    {
        private string path;
        private string title;
        private string artist;
        private string album;
        private int duration;

        /* Properties */
        public string Path { get { return path; } set { path = value; } }
        public string Title { get { return title; } set { title = value; } }
        public string Artist { get { return artist; } set { artist = value; } }
        public string Album { get { return album; } set { album = value; } }
        public int Duration { get { return duration; } set { duration = value; } }

        public Song(string path_, string title_, string artist_, string album_)
        {
            path = path_;
            title = title_;
            artist = artist_;
            album = album_;
            if (title == null)
                title = System.IO.Path.GetFileNameWithoutExtension(path);
            if (artist == null)
                artist = " ";
            if (album == null)
                album = "Unknown Album";
        }
    }
}
