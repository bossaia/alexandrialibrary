using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Alexandria;

namespace Alexandria.MusicBrainz
{
	//[MetadataProviderClass]
	public class MusicBrainzMetadataProvider : IAlbumFactory
	{
		#region Private Constant Fields
		private const int CDINDEX_ID_LEN = 28;
		#endregion
	
		#region Private Fields
		private Rdf rdf = new Rdf();
		#endregion

		#region Private Methods

		#region LookupAlbumById
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		private IAlbum LookupAlbumById(string id)
		{
			IAlbum album = null;

			StringBuilder output = new StringBuilder();

			string data, temp = null;
			bool ret, isMultipleArtist = false;
			int numTracks, trackNum = 0;
			//string id = //"cCQzv12PgDzszIK8_ZLHSK7oHJc-";


			// Create the musicbrainz object, which will be needed for subsequent calls
			MusicBrainzClient o = new MusicBrainzClient();
			//Client o = new Client();

			// Set the proper server to use. Defaults to mm.musicbrainz.org:80
			if (Environment.GetEnvironmentVariable("MB_SERVER") != null)
				o.Server = new ServerInfo(Environment.GetEnvironmentVariable("MB_SERVER"), 80);

			// Check to see if the debug env var has been set 
			if (Environment.GetEnvironmentVariable("MB_DEBUG") != null)
				o.Debug = (Environment.GetEnvironmentVariable("MB_DEBUG") != "0");

			// Tell the server to only return 2 levels of data, unless the MB_DEPTH env var is set
			if (Environment.GetEnvironmentVariable("MB_DEPTH") != null)
				o.QueryDepth = (int.Parse(Environment.GetEnvironmentVariable("MB_DEPTH"), System.Globalization.CultureInfo.InvariantCulture));
			else
				o.QueryDepth = 4;

			// Set up the args for the find album query
			string[] args = new string[] { id };

			if (id.Length != CDINDEX_ID_LEN)
				// Execute the MBQ_GetAlbumById query
				ret = o.Query(rdf.QueryGetAlbumById, args);
			else
				// Execute the MBQ_GetCDInfoFromCDIndexId
				ret = o.Query(rdf.QueryGetCDInfoFromCDIndexId, args);

			if (!ret)
			{
				//o.GetQueryError(out error);
				System.Diagnostics.Debug.WriteLine("Query failed: {0}", o.QueryError);
				return album; //o.QueryError;
			}

			// Select the first album
			o.Select(rdf.SelectAlbum, 1);

			// Pull back the album id to see if we got the album
			data = o.GetResultData(rdf.ExpressionAlbumGetAlbumId);
			if (o.CurrentResult == 0)
			{
				System.Diagnostics.Debug.WriteLine("Album not found.");
				return album; //string.Empty;
			}
			temp = o.GetIdFromUrl(data);
			System.Diagnostics.Debug.WriteLine("    AlbumId: {0}", temp);
			output.AppendLine("AlbumId: " + temp);

			// Extract the album name
			data = o.GetResultData(rdf.ExpressionAlbumGetAlbumName);
			if (o.CurrentResult != 0)
			{
				System.Diagnostics.Debug.WriteLine("       Name: {0}", data);
				output.AppendLine("Name: " + data);
			}

			// Extract the number of tracks
			numTracks = o.GetResultInt(rdf.ExpressionAlbumGetNumberTracks);
			if (numTracks > 0 && numTracks < 100)
			{
				System.Diagnostics.Debug.WriteLine("  NumTracks: {0}", numTracks.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
				output.AppendLine("Tracks: " + numTracks.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}

			// Check to see if there is more than one artist for this album
			for (int i = 1; i <= numTracks; i++)
			{
				data = o.GetResultData(rdf.ExpressionAlbumGetArtistId, i);
				if (o.CurrentResult == 0)
					break;

				if (i == 1)
					temp = data;

				if (temp != data)
				{
					isMultipleArtist = true;
					break;
				}
			}

			if (!isMultipleArtist)
			{
				// Extract the artist name from the album
				data = o.GetResultData(rdf.ExpressionAlbumGetArtistName, 1);
				if (o.CurrentResult != 0)
				{
					System.Diagnostics.Debug.WriteLine("AlbumArtist: {0}", data);
					output.AppendLine("Album Artist: " + data);
				}

				// Extract the artist id from the album
				data = o.GetResultData(rdf.ExpressionAlbumGetArtistId, 1);
				if (o.CurrentResult != 0)
				{
					temp = o.GetIdFromUrl(data);
					System.Diagnostics.Debug.WriteLine("   ArtistId: {0}", temp);
					output.AppendLine("ArtistId: " + temp);
				}
			}

			System.Diagnostics.Debug.WriteLine(string.Empty);

			for (int i = 1; i <= numTracks; i++)
			{
				// Extract the track name from the album.
				data = o.GetResultData(rdf.ExpressionAlbumGetTrackName, i);
				if (o.CurrentResult != 0)
				{
					System.Diagnostics.Debug.WriteLine("      Track: {0}", data);
					output.AppendLine("Track: " + data);
				}
				else
					break;

				// Extract the album id from the track. Just use the
				// first album that this track appears on
				data = o.GetResultData(rdf.ExpressionAlbumGetTrackId, i);
				if (o.CurrentResult != 0)
				{
					temp = o.GetIdFromUrl(data);
					System.Diagnostics.Debug.WriteLine("    TrackId: {0}", temp);
					output.AppendLine("TrackId: " + temp);

					// Extract the track number
					trackNum = o.GetOrdinalFromList(rdf.ExpressionAlbumGetTrackList, data);
					if (trackNum > 0 && trackNum < 100)
					{
						System.Diagnostics.Debug.WriteLine("  TrackNum: {0}", trackNum.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
						output.AppendLine("Track Number: " + trackNum.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
					}
				}

				// If its a multple artist album, print out the artist for each track
				if (isMultipleArtist)
				{
					// Extract the artist name from this track
					data = o.GetResultData(rdf.ExpressionAlbumGetArtistName, i);
					if (o.CurrentResult != 0)
					{
						System.Diagnostics.Debug.WriteLine("TrackArtist: {0}", data);
						output.AppendLine("Track Artist: " + data);
					}

					// Extract the artist id from this track
					data = o.GetResultData(rdf.ExpressionAlbumGetArtistId, i);
					if (o.CurrentResult != 0)
					{
						temp = o.GetIdFromUrl(data);
						System.Diagnostics.Debug.WriteLine("   ArtistId: {0}", temp);
						output.AppendLine("Track ArtistId: " + temp);
					}
				}
				System.Diagnostics.Debug.WriteLine(string.Empty);
			}

			//return output.ToString();
			return album;
		}
		#endregion
		
		#endregion

		#region Private Static Methods
		
		#region LookupCd
		private static IAlbum ReadCompactDisc(IAudioCompactDisc disc)
		{
			IAlbum album = null;
			
			if (disc != null)
			{							
				try
				{
					Debug.WriteLine("Drive Name:" + disc.Location.LocalPath);
				
					//"/dev/hdc" or D:
					string driveName = disc.Location.LocalPath;
					if (driveName.IndexOf(@"\") > -1)
					{
						driveName = driveName.Replace(@"\", string.Empty);
					}
					Debug.WriteLine("Cleaned up Drive Name: " + driveName);
					
					using (SimpleDisc simpleDisc = new SimpleDisc(driveName))
					{

						// Actually ask the MB server for metadata. As soon as a SimpleDisc is instantiated,
						// a disc layout is created based on reading the CD TOC directly. This query updates
						// that layout. For applications where UI must be responsive, run this query in
						// a thread.

						// To set proxy server information, call disc.Client.SetProxy(server, port) before
						// calling this query

						Debug.WriteLine("Before query CD metadata");
						simpleDisc.QueryCDMetadata();
						Debug.WriteLine("After query CD metadata");

						Debug.WriteLine("Artist Name   : " + simpleDisc.ArtistName);
						Debug.WriteLine("Album Name    : " + simpleDisc.AlbumName);
						Debug.WriteLine("Cover Art URL : " + simpleDisc.CoverArtUrl);
						Debug.WriteLine("Amazon ASIN   : " + simpleDisc.AmazonAsin);
						Debug.WriteLine("Release Date  : " + simpleDisc.ReleaseDate);
						Debug.WriteLine("");

						/*
						AlexandriaOrg.Alexandria.Data.Artist artist = new AlexandriaOrg.Alexandria.Data.Artist();
						artist.Name = disc.ArtistName;

						System.Diagnostics.Debug.WriteLine("AlbumId=" + disc.AlbumId);					
						album = new AlexandriaOrg.Alexandria.Data.Album();
						album.MusicBrainzId = disc.AlbumId;
						album.MusicBrainzUrl = disc.AlbumUrl;
						album.Name = disc.AlbumName;
						album.Artist = artist;
						album.AmazonAsin = disc.AmazonAsin;
						album.CoverArtUrl = disc.CoverArtUrl;

						foreach (SimpleTrack simpleTrack in disc)
						{
							string trackInfo = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:00}: {1} - {2} {3}:{4:00} ({5})",
								simpleTrack.Index, simpleTrack.Artist, simpleTrack.Title,
								simpleTrack.Minutes, simpleTrack.Seconds, simpleTrack.Length);
							Debug.WriteLine(trackInfo);
							Alexandria.Data.Track track = new AlexandriaOrg.Alexandria.Data.Track();
							
							Alexandria.Data.Artist trackArtist = new AlexandriaOrg.Alexandria.Data.Artist();
							trackArtist.Name = simpleTrack.Artist;
							
							track.Album = album;
							track.Artist = trackArtist;
							track.Length = (uint)simpleTrack.Length;
							track.Name = simpleTrack.Title;
							track.Milliseconds = Convert.ToUInt32((simpleTrack.Minutes * 60000) + (simpleTrack.Seconds * 1000));
							track.Number = Convert.ToUInt32(simpleTrack.Index);
							album.Tracks.Add(track.Number, track);
						}
						*/
					}
				}
				catch (Exception ex)
				{
					//Debug.WriteLine("Error reading from CD: " + ex.Message);
					throw new AlexandriaException("Error reading from CD", ex);
				}
			}
			else throw new ArgumentNullException("drive");
			
			return album;
		}
		#endregion
		
		#endregion
	
		#region Constructors
		public MusicBrainzMetadataProvider() : base()
		{
		}
		#endregion
		
		#region IAlbumFactory Members
		public IAlbum GetAlbum(IAudioCompactDisc disc)
		{
			return ReadCompactDisc(disc);
		}

		public IAlbum GetAlbum(ISearch albumSearch)
		{
			return null;
		}
		#endregion

		#region Public Methods
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public IAlbum GetAlbum(string id)
		{
			return LookupAlbumById(id);
		}
		#endregion

		#region IResource Members

		public IIdentifier Id
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public ILocation Location
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IMediaFormat Format
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		#endregion

		#region INamedResource Members

		public string Name
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public Version Version
		{
			get { return new Version(); }
		}

		#endregion

		#region IProxyResource Members

		public void Load()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
