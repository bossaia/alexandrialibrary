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
using Alexandria;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.MusicBrainz
{
	[Record("Album")]
	[RecordType("B0B28FDF-B65E-4d9f-8C53-6EFE6C087C4E")]
    internal class SimpleAlbum : IOldAlbum
    {
		#region Constructors
		public SimpleAlbum(Guid id, Uri path, string name, string artist, DateTime releaseDate)
		{
			this.id = id;
			this.path = path;
			this.name = name;
			this.artist = artist;
			this.releaseDate = releaseDate;
		}
		#endregion
    
		#region Private Fields
		private Guid id;
		private IRecord parent;
		private IPersistenceBroker broker;
		
		private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		private Uri path;
		private string name;
				
		private string artist;
		private DateTime releaseDate;
		
        private string musicBrainzId;
        private string title;
        private string asin;
        private Uri coverArtUrl;
        private bool variousArtists;
        private SimpleArtist albumArtist;
        private IList<IAudioTrack> tracks = new List<IAudioTrack>();
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
			builder.AppendFormat("Release Date:    {0}\n", ReleaseDate);
            builder.Append("Tracks:\n");
            
            foreach(SimpleTrack track in tracks)
                builder.AppendFormat("{0}\n", track);
            
            return builder.ToString();
        }
        #endregion
        
        #region Public Properties
        public string MusicBrainzId
        {
            get {return musicBrainzId;}
            internal set { musicBrainzId = value; }
        }
        
        public string Title
        {
            get {return title;}
            internal set { title = value; }
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
        
        public DateTime ReleaseDate
        {
            get {return releaseDate;}
        }
        
        public IList<IAudioTrack> Tracks
        {
            get {return tracks;}
        }
        #endregion

		#region IAlbum Members
		string IOldAlbum.Artist
		{
			get { return artist; }
		}

		IList<IAudioTrack> IOldAlbum.Tracks
		{
			get { return tracks; }
		}
		#endregion

		#region IMetadata Members
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public string Name
		{
			get { return name; }
		}
		#endregion

		#region IRecord Members
		Guid Alexandria.Persistence.IRecord.Id
		{
			get { return id; }
		}

		public Alexandria.Persistence.IRecord Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		public Alexandria.Persistence.IPersistenceBroker PersistenceBroker
		{
			get { return broker; }
			set { broker = value; }
		}

		public bool IsProxy
		{
			get { return false; }
		}

		public void Save()
		{
			if (broker != null)
				broker.SaveRecord(this);
		}

		public void Delete()
		{
			if (broker != null)
				broker.DeleteRecord(this);
		}
		#endregion
	}
}
