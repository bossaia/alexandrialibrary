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

namespace Alexandria.MusicBrainz
{
    public class SimpleAlbum
    {
		#region Private Fields
        private string id;
        private string title;
        private string asin;
        private Uri cover_art_url;
        private bool various_artists;
        private DateTime release_date = DateTime.MinValue;
        private SimpleArtist artist;
        private List<SimpleTrack> tracks;
        private Rdf rdf = new Rdf();
        #endregion
        
        #region Constructors
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public SimpleAlbum(MusicBrainzClient client, string id)
        {
			System.Diagnostics.Debug.WriteLine("Parameter AlbumID=" + id);
        
            client.QueryDepth = 4;
            id = client.GetId(id);
            
            System.Diagnostics.Debug.WriteLine("Client AlbumID=" + id);

			bool query = client.Query(rdf.QueryGetAlbumById, new string[] { id });
			bool select = client.Select(rdf.SelectAlbum, 1);
            
            if(!query || !select)
            {
				string error = client.QueryError;
				System.Diagnostics.Debug.WriteLine("query=" + query.ToString() + " select=" + select.ToString() + " error=" + error);
				// could not fetch album
                throw new AlexandriaException("Could not fetch album");
            }

			this.id = client.GetId(client.GetResultData(rdf.ExpressionAlbumGetAlbumId));
			asin = client.GetResultData(rdf.ExpressionAlbumGetAmazonAsin);
			cover_art_url = new Uri(client.GetResultData(rdf.ExpressionAlbumGetAmazonCoverArtUrl));
			string artist_name = client.GetResultData(rdf.ExpressionAlbumGetArtistName);
			string artistSortName = client.GetResultData(rdf.ExpressionAlbumGetArtistSortName);
			string artistId = client.GetResultData(rdf.ExpressionAlbumGetAlbumArtistId);
			if (artistId == rdf.IdVariousArtist) various_artists = true;  
            artist = new SimpleArtist(artistId, artist_name, artistSortName);          
            
            Console.WriteLine(artist_name);
			title = client.GetResultData(rdf.ExpressionAlbumGetAlbumName);

			int track_count = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks, 1);
            
            if(track_count <= 0)
            {
				// Invalid track count from album query            
                throw new AlexandriaException("Invalid track count from album query");
            }
                        
            tracks = new List<SimpleTrack>(track_count);//[track_count];
            
            for(int i = 1; i <= tracks.Count; i++) {
				client.Select(rdf.SelectTrack, i);
                
                tracks[i - 1] = new SimpleTrack(i, 0);
				tracks[i - 1].Artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
				tracks[i - 1].Title = client.GetResultData(rdf.ExpressionTrackGetTrackName);

				int length = client.GetResultInt(rdf.ExpressionTrackGetTrackDuration);
                tracks[i - 1].Length = length / 1000;

				client.Select(rdf.SelectBack);
            }
           
            if(client.GetResultInt(rdf.ExpressionAlbumGetNumberReleaseDates) > 0) {
				client.Select(rdf.SelectReleaseDate, 1);
				release_date = MusicBrainzUtility.ToDateTime(client.GetResultData(rdf.ExpressionReleaseGetDate));
				client.Select(rdf.SelectBack);
            }

			client.Select(rdf.SelectBack);
        }
        #endregion
        
        #region Public Methods
        public override string ToString()
        {
            string ret = String.Empty;
            
            ret += "ID:              " + Id + "\n";
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
        public string Id
        {
            get {return id;}
        }
        
        public string Title
        {
            get {return title;}
        }
        
        public SimpleArtist Artist
        {
            get {return artist;}
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
            get {return release_date;}
        }
        
        public IList<SimpleTrack> Tracks
        {
            get {return tracks;}
        }
        #endregion
    }
}
