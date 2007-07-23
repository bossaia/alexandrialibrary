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
using Alexandria;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.MusicBrainz
{
	[Record("Album")]
	[RecordType("B0B28FDF-B65E-4d9f-8C53-6EFE6C087C4E")]
    public class SimpleAlbum : IAlbum
    {
		#region Constructors
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public SimpleAlbum(MusicBrainzClient client, string musicBrainzId, Uri path)
		{
			this.path = path;
			Load(client, musicBrainzId);
		}
		
		[Factory("B0B28FDF-B65E-4d9f-8C53-6EFE6C087C4E")]
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
        private Uri cover_art_url;
        private bool various_artists;
        //private DateTime release_date = DateTime.MinValue;
        private SimpleArtist album_artist;
        private IList<IAudioTrack> tracks = new List<IAudioTrack>();
        private Rdf rdf = new Rdf();
        #endregion
        
        #region Private Methods
        //TODO: move this to an AlbumFactory class
        private void Load(MusicBrainzClient client, string musicBrainzId)
        {
			System.Diagnostics.Debug.WriteLine("Parameter AlbumID=" + musicBrainzId);

			client.QueryDepth = 4;
			musicBrainzId = client.GetId(musicBrainzId);

			System.Diagnostics.Debug.WriteLine("Client AlbumID=" + musicBrainzId);

			bool query = client.Query(rdf.QueryGetAlbumById, new string[] { musicBrainzId });
			bool select = client.Select(rdf.SelectAlbum, 1);

			if (!query || !select)
			{
				string error = client.QueryError;
				System.Diagnostics.Debug.WriteLine("query=" + query.ToString() + " select=" + select.ToString() + " error=" + error);
				// could not fetch album
				throw new AlexandriaException("Could not fetch album");
			}

			this.musicBrainzId = client.GetId(client.GetResultData(rdf.ExpressionAlbumGetAlbumId));
			asin = client.GetResultData(rdf.ExpressionAlbumGetAmazonAsin);
			cover_art_url = new Uri(client.GetResultData(rdf.ExpressionAlbumGetAmazonCoverArtUrl));
			string artist_name = client.GetResultData(rdf.ExpressionAlbumGetArtistName);
			string artistSortName = client.GetResultData(rdf.ExpressionAlbumGetArtistSortName);
			string artistId = client.GetResultData(rdf.ExpressionAlbumGetAlbumArtistId);
			if (artistId == rdf.IdVariousArtist) various_artists = true;
			album_artist = new SimpleArtist(artistId, artist_name, artistSortName);

			Console.WriteLine(artist_name);
			title = client.GetResultData(rdf.ExpressionAlbumGetAlbumName);

			int track_count = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks, 1);

			if (track_count <= 0)
			{
				// Invalid track count from album query            
				throw new AlexandriaException("Invalid track count from album query");
			}

			if (client.GetResultInt(rdf.ExpressionAlbumGetNumberReleaseDates) > 0)
			{
				client.Select(rdf.SelectReleaseDate, 1);
				releaseDate = MusicBrainzUtility.ToDateTime(client.GetResultData(rdf.ExpressionReleaseGetDate));
				client.Select(rdf.SelectBack);
			}

			tracks = new List<IAudioTrack>(track_count);//[track_count];

			for (int i = 1; i <= tracks.Count; i++)
			{
				client.Select(rdf.SelectTrack, i);

				string album = null; //TODO: figure out how to get album here
				string artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
				string name = client.GetResultData(rdf.ExpressionTrackGetTrackName);
				int milliseconds = (client.GetResultInt(rdf.ExpressionTrackGetTrackDuration) / 1000);
				TimeSpan duration = new TimeSpan(0, 0, 0, 0 , milliseconds);
				SimpleTrack track = new SimpleTrack(Guid.NewGuid(), path, name, album, artist, duration, releaseDate, i);
				tracks.Add(track);

				//tracks[i - 1] = new SimpleTrack(i, 0);
				//tracks[i - 1].Artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
				//tracks[i - 1].Title = client.GetResultData(rdf.ExpressionTrackGetTrackName);

				//int length = client.GetResultInt(rdf.ExpressionTrackGetTrackDuration);
				//tracks[i - 1].Length = length / 1000;

				client.Select(rdf.SelectBack);
			}

			client.Select(rdf.SelectBack);
        }
        #endregion
        
        #region Public Methods
        public override string ToString()
        {
            string ret = String.Empty;
            
            ret += "ID:              " + MusicBrainzId + "\n";
            ret += "Album Title:     " + Title + "\n";
            ret += "Amazon ASIN:     " + Asin + "\n";
            ret += "Cover Art:       " + CoverArtUrl + "\n";
            ret += "Various Artists: " + VariousArtists + "\n";
            ret += "Release Date:    " + ReleaseDate + "\n";
            
            ret += "Tracks:\n";
            
            foreach(SimpleTrack track in tracks) {
                ret += track + "\n";
            }
            
            return ret;
        }
        #endregion
        
        #region Public Properties
        public string MusicBrainzId
        {
            get {return musicBrainzId;}
        }
        
        public string Title
        {
            get {return title;}
        }
        
        public SimpleArtist AlbumArtist
        {
            get {return album_artist;}
        }
        
        public string Asin
        {
            get {return asin;}
        }
        
        public Uri CoverArtUrl
        {
            get {return cover_art_url;}
        }
        
        public bool VariousArtists
        {
            get {return various_artists;}
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
		string IAlbum.Artist
		{
			get { return artist; }
		}

		IList<IAudioTrack> IAlbum.Tracks
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
