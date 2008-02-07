#region License (MIT)
/***************************************************************************
 *  SimpleDisc.cs
 *  Copyright (C) 2006 Dan Poage
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
using System.Diagnostics;

namespace Telesophy.Alexandria.MusicBrainz
{
    public class SimpleDisc : IEnumerable<SimpleTrack>, IDisposable
    {
		#region Constructors
		public SimpleDisc(string device, MusicBrainzClient client)
		{
			this.client = client;
			client.Device = device;

			Debug.WriteLine("Before ReadCDToc");

			ReadCDToc();

			Debug.WriteLine("After ReadCDToc lengths.Length=" + lengths.Length.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

			tracks = new List<SimpleTrack>(lengths.Length);

			Debug.WriteLine("tracks.Count=" + tracks.Count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

			for (int i = 0; i < lengths.Length; i++)
			{
				Debug.WriteLine("i=" + i.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
				if (lengths != null)
					Debug.WriteLine("lengths[i]=" + lengths[i].ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
				else
					Debug.WriteLine("lengths array is null");

				try
				{
					tracks.Add(new SimpleTrack());
					//tracks.Add(new SimpleTrack(i + 1, lengths[i]));
				}
				catch (IndexOutOfRangeException)
				{
					Debug.WriteLine("index out of range trying to initialize tracks");
				}
				catch (ArgumentOutOfRangeException)
				{
					Debug.WriteLine("argument out of range trying to initialize tracks");
				}
			}
		}

		public SimpleDisc(string device) : this(device, new MusicBrainzClient())
		{

		}
		#endregion
        
        #region Finalizer
        ~SimpleDisc()
        {
			Dispose(false);
        }
        #endregion
        
        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
			if (disposing)
			{
				client.Dispose();
			}
        }
        
        public void Dispose()
        {
			Dispose(true);
			GC.SuppressFinalize(this);
        }
        #endregion

		#region Private Fields
		private MusicBrainzClient client;
		private List<SimpleTrack> tracks;
		private int[] lengths;
		private string artistName;
		private System.Uri albumUrl;
		private string albumId;
		private string albumName;
		private System.Uri coverArtUrl;
		private string amazonAsin;
		private DateTime releaseDate = DateTime.MinValue;
		private Rdf rdf = new Rdf();
		#endregion
        
        #region Private Methods
        private static int SectorsToSeconds(int sectors)
        {
            return sectors * 2352 / (44100 / 8) / 16 / 2;
        }
        
        private void ReadCDToc()
        {
			client.Query(rdf.QueryGetCDToc);
			int track_count = client.GetResultInt(rdf.ExpressionTocGetLastTrack);
            
            if(track_count <= 0)
            {
				// "Reading audio CD Table of Contents returned an invalid track count."
                throw new ApplicationException("Reading audio CD Table of Contents returned an invalid track count.");
            }
            
            lengths = new int[track_count];
            
            for(int i = 1; i <= lengths.Length; i++)
            {
				lengths[i - 1] = SectorsToSeconds(client.GetResultInt(rdf.ExpressionTocGetTrackNumberSectors, i + 1));
            }
        }
        #endregion
        
        #region Public Methods
        public void QueryCDMetadata()
        {
			if (!client.Query(rdf.QueryGetCDInfo))
			{
				// "Could not query CD"
				Debug.WriteLine("Could not query CD");
                throw new ApplicationException("Could not query CD");
            }

			int disc_count = client.GetResultInt(rdf.ExpressionGetNumberAlbums);
            
            if(disc_count <= 0)
            {
				// "No Discs Found"
				Debug.WriteLine("No Discs Found");
                throw new ApplicationException("No Discs Found");
            }
            
            // handle more than 1 disc? not sure how this works? for multi-disc sets? 
            // if that's the case, we only care about the first one I guess... I hope

			if (!client.Select(rdf.SelectAlbum, 1))
			{
				// "Could not select disc"
				Debug.WriteLine("Could not select disc");
                throw new ApplicationException("Could not select disc");
            }

			amazonAsin = client.GetResultData(rdf.ExpressionAlbumGetAmazonAsin, 1);
            coverArtUrl = new Uri(rdf.AmazonCoverArtUrlPrefix + amazonAsin);
			artistName = client.GetResultData(rdf.ExpressionAlbumGetArtistName, 1);
			albumUrl = new Uri(client.GetResultData(rdf.ExpressionAlbumGetAlbumId));
            albumId = client.GetIdFromUrl(albumUrl);
			albumName = client.GetResultData(rdf.ExpressionAlbumGetAlbumName, 1);
            
			int num_releases = client.GetResultInt(rdf.ExpressionAlbumGetNumberReleaseDates);
            
            if(num_releases > 0) {
				client.Select(rdf.SelectReleaseDate, 1);

				string raw_date = client.GetResultData(rdf.ExpressionReleaseGetDate);
                if(raw_date != null && raw_date.Length > 0)
                {
                    string [] parts = raw_date.Split('-');
                    if(parts.Length == 3)
                    {
                        int year = Convert.ToInt32(parts[0], System.Globalization.NumberFormatInfo.InvariantInfo);
						int month = Convert.ToInt32(parts[1], System.Globalization.NumberFormatInfo.InvariantInfo);
						int day = Convert.ToInt32(parts[2], System.Globalization.NumberFormatInfo.InvariantInfo);
                        
                        releaseDate = new DateTime(year, month, day);
                    } 
                }

				client.Select(rdf.SelectBack);
            }

			int trackCount = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks, 1);
			if (trackCount <= 0)
			{
				// "Invalid track count from album query"
				Debug.WriteLine("Invalid track count from album query");
				throw new ApplicationException("Invalid track count from album query");
			}

			tracks = new List<SimpleTrack>(trackCount);

			string pathRoot = string.Format("file://{0}{1}", client.Device, System.IO.Path.DirectorySeparatorChar);

			for (int i = 1; i <= trackCount; i++)
			{
				client.Select(rdf.SelectTrack, i);

				//[i - 1] = new SimpleTrack(); //new SimpleTrack(i, lengths[i - 1]);
				string artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
				string title = client.GetResultData(rdf.ExpressionTrackGetTrackName);
				int trackNumber = i;
				Uri path = new Uri(string.Format("{0}Track {1}", pathRoot, i));

				TimeSpan duration = TimeSpan.Zero;
				int milliseconds = client.GetResultInt(rdf.ExpressionTrackGetTrackDuration);
				if (milliseconds > 0)
				{
					//duration = duration / 1000;
					duration = new TimeSpan(0, 0, 0, 0, milliseconds);
				}

				SimpleTrack track = new SimpleTrack(Guid.NewGuid(), trackNumber, title, artist, albumName, duration, releaseDate, MusicBrainzConstants.FORMAT_CD, path);
				tracks.Add(track);

				client.Select(rdf.SelectBack);
			}

			client.Select(rdf.SelectBack);
        }
        
        [CLSCompliant(false)]
        public SimpleTrack this[int index]
        {
            get {return tracks[index];}
        }

		/*
		public System.Collections.IEnumerator GetEnumerator()
        {
            foreach(SimpleTrack track in Tracks)
            {
                yield return track;
            }
        }
		*/
        
        public IList<SimpleTrack> Tracks
        {
            get {return tracks;}
        }
        
		public System.Uri CoverArtUrl
		{
			get {return coverArtUrl;}
		}
        
		public string AmazonAsin
		{
			get {return amazonAsin;}
		}
        
        public string AlbumId
        {
			get {return albumId;}
        }
        
        public System.Uri AlbumUrl
        {
			get {return albumUrl;}
        }
        
		public string AlbumName
		{
			get {return albumName;}
		}
        
		public string ArtistName
		{
			get {return artistName;}
		} 
        
		public DateTime ReleaseDate
		{
			get {return releaseDate;}
		}

		public MusicBrainzClient Client
		{
			get {return client;}
		}
		#endregion

		#region IEnumerable<SimpleTrack> Members
		public IEnumerator<SimpleTrack> GetEnumerator()
		{
			foreach (SimpleTrack track in Tracks)
			{
				yield return track;
			}
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (SimpleTrack track in Tracks)
			{
				yield return track;
			}
		}
		#endregion
	}
}
