#region License (MIT)
/***************************************************************************
 *  SimpleAlbum.cs
 *
 *  Copyright (C) 2005 Novell
 *  Written by Aaron Bockover (aaron@aaronbock.net)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Telesophy.Alexandria.Model;

namespace Telesophy.Alexandria.MusicBrainz
{
    internal class SimpleAlbum : Album
    {
		#region Constructors
		public SimpleAlbum(Guid id, string title, string artist, DateTime date, Uri path)
			: base(id, MusicBrainzConstants.SOURCE, 0, title, artist, date, "audio/cdda", path, null)
		{
		}
		#endregion
    
		#region Private Fields
        private string musicBrainzId;
        //private string title;
        private string asin;
        private Uri coverArtUrl;
        private bool variousArtists;
        private SimpleArtist albumArtist;
        #endregion
                
        #region Public Properties
        public string MusicBrainzId
        {
            get {return musicBrainzId;}
            internal set { musicBrainzId = value; }
        }
        
        public SimpleArtist AlbumArtist
        {
            get {return albumArtist;}
            internal set { albumArtist = value; }
        }
        
        public string Asin
        {
            get {return asin;}
            internal set { asin = value; }
        }
        
        public Uri CoverArtUrl
        {
            get {return coverArtUrl;}
            internal set { coverArtUrl = value; }
        }
        
        public bool VariousArtists
        {
            get { return variousArtists; }
            set { variousArtists = value; }
        }
        #endregion
        
        #region Public Methods
        public override string ToString()
        {
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("ID:              {0}\n", MusicBrainzId);
            builder.AppendFormat("Album Title:     {0}\n", Title);
            builder.AppendFormat("Amazon ASIN:     {0}\n", Asin);
            builder.AppendFormat("Cover Art:       {0}\n", CoverArtUrl);
            builder.AppendFormat("Various Artists: {0}\n", VariousArtists);
			builder.AppendFormat("Release Date:    {0}\n", Date);
            builder.Append("Tracks:\n");
            
            //foreach(SimpleTrack track in tracks)
                //builder.AppendFormat("{0}\n", track);
            
            return builder.ToString();
        }
        #endregion
	}
}
