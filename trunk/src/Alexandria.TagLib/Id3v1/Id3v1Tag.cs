/***************************************************************************
    copyright            : (C) 2006 by Dan Poage
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : id3v1tag.cpp from TagLib
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class Id3v1Tag : Tag
	{
		#region Constructors
		public Id3v1Tag()
		{
			//file = null;
			tagOffset = -1;

			title = artist = album = year = comment = string.Empty;

			//track = 0;
			genre = 255;
		}

		public Id3v1Tag(File file, long tagOffset)
		{
			this.file = file;
			this.tagOffset = tagOffset;

			Read();
		}
		#endregion
	
		#region Private Fields
		private File file;
		private long tagOffset;
		private string title;
		private string artist;
		private string album;
		private string year;
		private string comment;
		private byte track;
		private byte genre;
		private static Id3v1StringHandler stringHandler = new Id3v1StringHandler();
		#endregion
      
		#region Private Static Fields
		private static ByteVector fileIdentifier = "TAG";
		#endregion
		
		#region Protected Methods
		protected void Read()
		{
			if (file != null && file.IsValid)
			{
				file.Seek(tagOffset);

				// read the tag -- always 128 buffer
				ByteVector data = file.ReadBlock(128);

				// some initial sanity checking
				if (data.Count == 128 && data.StartsWith(FileIdentifier)) //"TAG"))
					Parse(data);
				else
					TagLibDebugger.Debug("ID3v1 tag is not valid or could not be read at the specified offset.");
			}
		}

		protected void Parse(ByteVector data)
		{
			if (data != null)
			{
				int offset = 3;

				title = stringHandler.Parse(data.Mid(offset, 30));
				offset += 30;

				artist = stringHandler.Parse(data.Mid(offset, 30));
				offset += 30;

				album = stringHandler.Parse(data.Mid(offset, 30));
				offset += 30;

				year = stringHandler.Parse(data.Mid(offset, 4));
				offset += 4;

				// Check for ID3v1.1 -- Note that ID3v1 *does not* support "track zero" -- this
				// is not a bug in TagLib.  Since a zeroed byte is what we would expect to
				// indicate the end of a C-String, specifically the comment string, a value of
				// zero must be assumed to be just that.

				if (data[offset + 28] == 0 && data[offset + 29] != 0)
				{
					// ID3v1.1 detected

					comment = stringHandler.Parse(data.Mid(offset, 28));
					track = data[offset + 29];
				}
				else
					comment = stringHandler.Parse(data.Mid(offset, 30));

				offset += 30;

				genre = data[offset];
			}
			else throw new ArgumentNullException("data");
		}
		#endregion
		
		#region Public Properties
		public override string Title
		{
			get { return title; }
			set { title = value != null ? value.Trim() : ""; }
		}

		public override IList<string> Artists
		{
			get
			{
				List<string> artists = new List<string>();
				artists.Add(artist);
				return artists;
			}
			set { artist = (new StringCollection(value)).ToString(",").Trim(); }
		}

		public override string Album
		{
			get { return album; }
			set { album = value != null ? value.Trim() : ""; }
		}

		public override string Comment
		{
			get { return comment; }
			set { comment = value != null ? value.Trim() : ""; }
		}

		public override IList<string> Genres
		{
			get
			{
				string genreName = Id3v1GenreList.GetGenre(genre);
				List<string> genres = new List<string>();
				genres.Add(genreName);
				return genres;
			}
			set
			{
				if (value != null && value.Count > 0)
				{
					Id3v1GenreList.GetGenreIndex(value[0].Trim());
				}
				else genre = (byte)255;
			}
		}

		[System.CLSCompliant(false)]
		public override uint Year
		{
			get
			{
				uint yearNumber = 0;
				if (uint.TryParse(year, out yearNumber))
					return yearNumber;
				else
					return 0;
			}
			set { year = value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo); }
		}

		[System.CLSCompliant(false)]
		public override uint Track
		{
			get { return track; }
			set { track = (byte)value; }
		}		
		#endregion
      
		#region Public Static Properties
		public static ByteVector FileIdentifier
		{
			get {return fileIdentifier;}
		}		
      
		public static Id3v1StringHandler DefaultStringHandler
		{
			get {return stringHandler;}
			set {stringHandler = value;}
		}
		#endregion
      
		#region Public Methods
		public ByteVector Render()
		{
			ByteVector data = new ByteVector();

			data.Add(FileIdentifier);
			data.Add(stringHandler.Render(title).Resize(30));
			data.Add(stringHandler.Render(artist).Resize(30));
			data.Add(stringHandler.Render(album).Resize(30));
			data.Add(stringHandler.Render(year).Resize(4));
			data.Add(stringHandler.Render(comment).Resize(28));
			data.Add((byte)0);
			data.Add(track);
			data.Add(genre);

			return data;
		}
		#endregion
	}
}
