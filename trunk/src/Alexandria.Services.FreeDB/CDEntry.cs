#region COPYRIGHT (c) 2004 by Brian Weeres
/* Copyright (c) 2004 by Brian Weeres
 * 
 * Email: bweeres@protegra.com; bweeres@hotmail.com
 * 
 * Permission to use, copy, modify, and distribute this software for any
 * purpose is hereby granted, provided that the above
 * copyright notice and this permission notice appear in all copies.
 *
 * If you modify it then please indicate so. 
 *
 * The software is provided "AS IS" and there are no warranties or implied warranties.
 * In no event shall Brian Weeres and/or Protegra Technology Group be liable for any special, 
 * direct, indirect, or consequential damages or any damages whatsoever resulting for any reason 
 * out of the use or performance of this software
 * 
 */
#endregion
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

namespace Alexandria.FreeDB
{
	/// <summary>
	/// Summary description for CDEntry.
	/// </summary>
	public class CDEntry
	{
		#region Constructors
		public CDEntry(StringCollection data)
		{
			if (!Parse(data))
			{
				throw new AlexandriaException("Unable to Parse CDEntry.");
			}
		}
		#endregion
	
		#region Private Fields
		private string discId;
		private string artist;
		private string title;
		private string year;
		private string genre;
		private TrackCollection tracks = new TrackCollection(); // 0 based - first track is at 0 last track is at numtracks - 1
		private string extendedData;
		private string playOrder;
		#endregion

		#region Private Methods
		private bool Parse(StringCollection data)
		{
			foreach (string line in data)
			{

				// check for comment

				if (line[0] == '#')
					continue;

				int index = line.IndexOf('=');
				if (index == -1) // couldn't find equal sign have no clue what the data is
					continue;
				string field = line.Substring(0, index);
				index++; // move it past the equal sign

				switch (field)
				{
					case "DISCID":
						{
							discId = line.Substring(index);
							continue;
						}

					case "DTITLE": // artist / title
						{
							artist += line.Substring(index);
							continue;
						}

					case "DYEAR":
						{
							year = line.Substring(index);
							continue;
						}

					case "DGENRE":
						{
							genre += line.Substring(index);
							continue;
						}

					case "EXTD":
						{
							// may be more than one - just concatenate them
							extendedData += line.Substring(index);
							continue;
						}

					case "PLAYORDER":
						{
							playOrder += line.Substring(index);
							continue;
						}


					default:

						//get track info or extended track info
						if (field.StartsWith("TTITLE"))
						{
							int trackNumber = -1;
							// Parse could throw an exception
							try
							{
								trackNumber = int.Parse(field.Substring("TTITLE".Length));
							}

							catch (Exception ex)
							{
								Debug.WriteLine("Failed to parse track Number. Reason: " + ex.Message);
								continue;
							}

							//may need to concatenate track info
							if (trackNumber < tracks.Count)
								tracks[trackNumber].Title += line.Substring(index);
							else
							{
								Track track = new Track(line.Substring(index));
								tracks.Add(track);
							}
							continue;
						}
						else if (field.StartsWith("EXTT"))
						{
							int trackNumber = -1;
							// Parse could throw an exception
							try
							{
								trackNumber = int.Parse(field.Substring("EXTT".Length));
							}

							catch (Exception ex)
							{
								Debug.WriteLine("Failed to parse track Number. Reason: " + ex.Message);
								continue;
							}

							if (trackNumber < 0 || trackNumber > tracks.Count - 1)
								continue;

							tracks[trackNumber].ExtendedData += line.Substring(index);
						}
						continue;
				} //end of switch

			}


			//split the title and artist from DTITLE;
			// see if we have a slash
			int slash = artist.IndexOf(" / ");
			if (slash == -1)
			{
				title = artist;
			}
			else
			{
				string titleArtist = artist;
				artist = titleArtist.Substring(0, slash);
				slash += 3; // move past " / "
				title = titleArtist.Substring(slash);
			}

			return true;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Get or set the Disc ID
		/// </summary>
		public string Discid
		{
			get { return discId; }
			set	{ discId = value; }
		}

		/// <summary>
		/// Property Artist (string)
		/// </summary>
		public string Artist
		{
			get { return artist; }
			set	{ artist = value; }
		}

		/// <summary>
		/// Property Title (string)
		/// </summary>
		public string Title
		{
			get { return title; }
			set	{ title = value; }
		}

		/// <summary>
		/// Property Year (string)
		/// </summary>
		public string Year
		{
			get { return year; }
			set	{ year = value; }
		}

		/// <summary>
		/// Property Genre (string)
		/// </summary>
		public string Genre
		{
			get { return genre; }
			set	{ genre = value; }
		}

		/// <summary>
		/// Property Tracks (StringCollection)
		/// </summary>
		public TrackCollection Tracks
		{
			get { return tracks; }
			set	{ tracks = value; }
		}


		/// <summary>
		/// Property ExtendedData (string)
		/// </summary>
		public string ExtendedData
		{
			get { return extendedData; }
			set { extendedData = value; }
		}

		/// <summary>
		/// Property PlayOrder (string)
		/// </summary>
		public string PlayOrder
		{
			get { return playOrder; }
			set { playOrder = value; }
		}

		/// <summary>
		/// Property NumberOfTracks (int)
		/// </summary>
		public int NumberOfTracks
		{
			get
			{
				if (tracks != null)
					return tracks.Count;
				else return 0;
			}
		}
		#endregion

		#region Public Methods
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("Title: ");
			builder.Append(title);
			builder.Append("\n");
			builder.Append("Artist: ");
			builder.Append(artist);
			builder.Append("\n");
			builder.Append("Discid: ");
			builder.Append(discId);
			builder.Append("\n");
			builder.Append("Genre: ");
			builder.Append(genre);
			builder.Append("\n");
			builder.Append("Year: ");
			builder.Append(year);
			builder.Append("\n");
			builder.Append("Tracks:");
			foreach (Track track in tracks)
			{
				builder.Append("\n");
				builder.Append(track.Title);
			}

			return builder.ToString();
		}
		#endregion
	}
}
