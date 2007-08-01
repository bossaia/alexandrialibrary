using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using System.Text;
using Alexandria.Metadata;
using Alexandria.Persistence;

namespace Alexandria.MusicBrainz
{
	public class SimpleAlbumFactory
	{
		#region Constructors
		public SimpleAlbumFactory()
		{
		}
		#endregion
		
		#region Private Fields
		private MusicBrainzClient client = new MusicBrainzClient();
		private Rdf rdf = new Rdf();
		#endregion

		#region Private Constant Fields
		private const int CDINDEX_ID_LEN = 28;
		#endregion

		#region Private Methods

		#region DebugLookupAlbumById
		[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		private SimpleAlbum DebugLookupAlbumById(string id)
		{
			SimpleAlbum album = null;

			StringBuilder output = new StringBuilder();

			string data, temp = null;
			bool ret, isMultipleArtist = false;
			int numTracks, trackNum = 0;
			//string id = //"cCQzv12PgDzszIK8_ZLHSK7oHJc-";


			// Create the musicbrainz object, which will be needed for subsequent calls
			//MusicBrainzClient o = new MusicBrainzClient();
			//Client o = new Client();

			// Set the proper server to use. Defaults to mm.musicbrainz.org:80
			if (Environment.GetEnvironmentVariable("MB_SERVER") != null)
				client.Server = new ServerInfo(Environment.GetEnvironmentVariable("MB_SERVER"), 80);

			// Check to see if the debug env var has been set 
			if (Environment.GetEnvironmentVariable("MB_DEBUG") != null)
				client.Debug = (Environment.GetEnvironmentVariable("MB_DEBUG") != "0");

			// Tell the server to only return 2 levels of data, unless the MB_DEPTH env var is set
			if (Environment.GetEnvironmentVariable("MB_DEPTH") != null)
				client.QueryDepth = (int.Parse(Environment.GetEnvironmentVariable("MB_DEPTH"), System.Globalization.CultureInfo.InvariantCulture));
			else
				client.QueryDepth = 4;

			// Set up the args for the find album query
			string[] args = new string[] { id };

			if (id.Length != CDINDEX_ID_LEN)
				// Execute the MBQ_GetAlbumById query
				ret = client.Query(rdf.QueryGetAlbumById, args);
			else
				// Execute the MBQ_GetCDInfoFromCDIndexId
				ret = client.Query(rdf.QueryGetCDInfoFromCDIndexId, args);

			if (!ret)
			{
				//o.GetQueryError(out error);
				System.Diagnostics.Debug.WriteLine("Query failed: {0}", client.QueryError);
				return album; //o.QueryError;
			}

			// Select the first album
			client.Select(rdf.SelectAlbum, 1);

			// Pull back the album id to see if we got the album
			data = client.GetResultData(rdf.ExpressionAlbumGetAlbumId);
			if (client.CurrentResult == 0)
			{
				System.Diagnostics.Debug.WriteLine("Album not found.");
				return album; //string.Empty;
			}
			temp = client.GetIdFromUrl(data);
			System.Diagnostics.Debug.WriteLine("    AlbumId: {0}", temp);
			output.AppendLine("AlbumId: " + temp);

			// Extract the album name
			data = client.GetResultData(rdf.ExpressionAlbumGetAlbumName);
			if (client.CurrentResult != 0)
			{
				System.Diagnostics.Debug.WriteLine("       Name: {0}", data);
				output.AppendLine("Name: " + data);
			}

			// Extract the number of tracks
			numTracks = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks);
			if (numTracks > 0 && numTracks < 100)
			{
				System.Diagnostics.Debug.WriteLine("  NumTracks: {0}", numTracks.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
				output.AppendLine("Tracks: " + numTracks.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			}

			// Check to see if there is more than one artist for this album
			for (int i = 1; i <= numTracks; i++)
			{
				data = client.GetResultData(rdf.ExpressionAlbumGetArtistId, i);
				if (client.CurrentResult == 0)
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
				data = client.GetResultData(rdf.ExpressionAlbumGetArtistName, 1);
				if (client.CurrentResult != 0)
				{
					System.Diagnostics.Debug.WriteLine("AlbumArtist: {0}", data);
					output.AppendLine("Album Artist: " + data);
				}

				// Extract the artist id from the album
				data = client.GetResultData(rdf.ExpressionAlbumGetArtistId, 1);
				if (client.CurrentResult != 0)
				{
					temp = client.GetIdFromUrl(data);
					System.Diagnostics.Debug.WriteLine("   ArtistId: {0}", temp);
					output.AppendLine("ArtistId: " + temp);
				}
			}

			System.Diagnostics.Debug.WriteLine(string.Empty);

			for (int i = 1; i <= numTracks; i++)
			{
				// Extract the track name from the album.
				data = client.GetResultData(rdf.ExpressionAlbumGetTrackName, i);
				if (client.CurrentResult != 0)
				{
					System.Diagnostics.Debug.WriteLine("      Track: {0}", data);
					output.AppendLine("Track: " + data);
				}
				else
					break;

				// Extract the album id from the track. Just use the
				// first album that this track appears on
				data = client.GetResultData(rdf.ExpressionAlbumGetTrackId, i);
				if (client.CurrentResult != 0)
				{
					temp = client.GetIdFromUrl(data);
					System.Diagnostics.Debug.WriteLine("    TrackId: {0}", temp);
					output.AppendLine("TrackId: " + temp);

					// Extract the track number
					trackNum = client.GetOrdinalFromList(rdf.ExpressionAlbumGetTrackList, data);
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
					data = client.GetResultData(rdf.ExpressionAlbumGetArtistName, i);
					if (client.CurrentResult != 0)
					{
						System.Diagnostics.Debug.WriteLine("TrackArtist: {0}", data);
						output.AppendLine("Track Artist: " + data);
					}

					// Extract the artist id from this track
					data = client.GetResultData(rdf.ExpressionAlbumGetArtistId, i);
					if (client.CurrentResult != 0)
					{
						temp = client.GetIdFromUrl(data);
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
		
		#region LookupAlbumById
		private SimpleAlbum LookupAlbumById(string musicBrainzId, Uri path)
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

			//NOTE: does this need to be a different variable?
			musicBrainzId = client.GetId(client.GetResultData(rdf.ExpressionAlbumGetAlbumId));
			
			string asin = client.GetResultData(rdf.ExpressionAlbumGetAmazonAsin);
			Uri coverArtUrl = new Uri(client.GetResultData(rdf.ExpressionAlbumGetAmazonCoverArtUrl));
			string artistName = client.GetResultData(rdf.ExpressionAlbumGetArtistName);
			string artistSortName = client.GetResultData(rdf.ExpressionAlbumGetArtistSortName);
			string artistId = client.GetResultData(rdf.ExpressionAlbumGetAlbumArtistId);
			bool isVariousArtists = (artistId == rdf.IdVariousArtist);
			SimpleArtist albumArtist = new SimpleArtist(artistId, artistName, artistSortName);

			Console.WriteLine(artistName);
			string albumName = client.GetResultData(rdf.ExpressionAlbumGetAlbumName);

			int trackCount = client.GetResultInt(rdf.ExpressionAlbumGetNumberTracks, 1);

			if (trackCount <= 0)
			{
				// Invalid track count from album query            
				throw new AlexandriaException("Invalid track count from album query");
			}

			DateTime releaseDate = new DateTime(1900, 1, 1);
			if (client.GetResultInt(rdf.ExpressionAlbumGetNumberReleaseDates) > 0)
			{
				client.Select(rdf.SelectReleaseDate, 1);
				releaseDate = Utility.ToDateTime(client.GetResultData(rdf.ExpressionReleaseGetDate));
				client.Select(rdf.SelectBack);
			}
			
			SimpleAlbum album = new SimpleAlbum(Guid.NewGuid(), path, albumName, artistName, releaseDate);
			album.VariousArtists = isVariousArtists;

			for (int i = 1; i <= trackCount; i++)
			{
				client.Select(rdf.SelectTrack, i);
				string artist = client.GetResultData(rdf.ExpressionTrackGetArtistName);
				string name = client.GetResultData(rdf.ExpressionTrackGetTrackName);
				int milliseconds = (client.GetResultInt(rdf.ExpressionTrackGetTrackDuration) / 1000);
				TimeSpan duration = new TimeSpan(0, 0, 0, 0, milliseconds);
				SimpleTrack track = new SimpleTrack(Guid.NewGuid(), path, name, albumName, artist, duration, releaseDate, i);
				track.Parent = album;
				album.Tracks.Add(track);
				client.Select(rdf.SelectBack);
			}

			client.Select(rdf.SelectBack);
			
			return album;
		}
		#endregion
		
		#region LookupAlbumByCompactDisc
		private SimpleAlbum LookupAlbumByCompactDisc(Uri path)
		{
			SimpleAlbum album = null;

			if (path != null)
			{
				try
				{
					Debug.WriteLine("Drive Name:" + path.ToString());

					//"/dev/hdc" or D:
					string driveName = path.LocalPath;
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

						album = new SimpleAlbum(Guid.NewGuid(), path, simpleDisc.AlbumName, simpleDisc.ArtistName, simpleDisc.ReleaseDate);
						foreach (SimpleTrack track in simpleDisc.Tracks)
						{
							track.Parent = album;
							album.Tracks.Add(track);
						}

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
		
		#region Public Methods
		public IAlbum CreateSimpleAlbum(Uri path)
		{
			return LookupAlbumByCompactDisc(path);
		}
		
		//[EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		public IAlbum CreateSimpleAlbum(string musicBrainzId, Uri path)
		{
			return LookupAlbumById(musicBrainzId, path);
		}

		[Factory("B0B28FDF-B65E-4d9f-8C53-6EFE6C087C4E")]
		public IAlbum CreateSimpleAlbum(Guid id, Uri path, string name, string artist, DateTime releaseDate)
		{
			return new SimpleAlbum(id, path, name, artist, releaseDate);
		}
		#endregion
	}
}
