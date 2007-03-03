//Created by: Edward Knapp
//date created: 11/9/06
//date edited: 11/9/06

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alexandria.Playlist
{
    public class M3uPlaylist : MediaPlaylist
    {
        #region Constructors
        public M3uPlaylist(string path): base(path)
        {

        }
        #endregion

        #region Public Methods
        public override void Load()
        {
            FileInfo playlistInfo = new FileInfo(Path);
            StreamReader reader = playlistInfo.OpenText();
            while(!reader.EndOfStream)
            {
                string filePath = reader.ReadLine();
                if (filePath != null)
                {
                    MediaFile file = MediaFile.Load(playlistInfo, filePath);
                    if (file != null)
                    {
                        Files.Add(file);
                    }
                }
                
            }
           
        }
        #endregion
    }
}
