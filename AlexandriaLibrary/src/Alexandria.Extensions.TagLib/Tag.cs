#region License (LGPL)
/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
    based on             : tag.cpp from TagLib
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
 #endregion

using System;
using System.Collections.Generic;

using Telesophy.Alexandria.Model;

namespace Alexandria.TagLib
{
	public class Tag : IMediaItem
	{
		#region Constructors
		public Tag()
		{
		}
		#endregion
		
		#region Private Fields
		private Guid id;
		private string source;
		private string type;
		
		private string name;
		private Uri path;
		private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		
		private string album;
		private IList<string> artists = new List<string>();
		private TimeSpan duration;
		private DateTime releaseDate;
		private string format;
		private IMediaSet parent;

		private IList<string> performers = new List<string>();
		private IList<string> composers = new List<string>();
		private IList<string> genres = new List<string>();
		private string comment;
		private uint year;
		private uint track;
		private uint trackCount;
		private uint disc;
		private uint discCount;
		
		private IList<IPicture> pictures = new List<IPicture>();
		#endregion
		
		#region Private Static Methods
		private static string FirstInGroup(IList<string> group)
		{
			return (group == null || group.Count == 0) ? null : group[0];
		}

		private static string JoinGroup(IList<string> group)
		{
			return new StringCollection(group).ToString(", ");
		}		
		#endregion
		
		#region OLD CODE
		/*
		public virtual string Title { get {return null;} set {} }
		public virtual IList<string> Artists { get {return null;} set {} }
		public virtual IList<string> Performers { get { return null; } set { } }		
		public virtual IList<string> Composers { get { return null; } set { } }
		public virtual string Album { get {return null;} set {} }
		public virtual string Comment { get {return null;} set {} }
		public virtual IList<string> Genres { get { return null; } set { } }
		
		[CLSCompliant(false)]
		public virtual uint Year {get {return 0;}    set {}}
		
		[CLSCompliant(false)]
		public virtual uint Track
		{
			get {return 0;}
			set {}
		}
		
		[CLSCompliant(false)]
		public virtual uint      TrackCount {get {return 0;}    set {}}
		
		[CLSCompliant(false)]
		public virtual uint      Disc       {get {return 0;}    set {}}
		
		[CLSCompliant(false)]
		public virtual uint      DiscCount  {get {return 0;}    set {}}
      
		public virtual IList<IImage> Pictures { get { return new List<IImage>(); } set { } }

		public string FirstArtist { get { return FirstInGroup(Artists); } }
		public string FirstPerformer { get { return FirstInGroup(Performers); } }
		public string FirstComposer { get { return FirstInGroup(Composers); } }
		public string FirstGenre { get { return FirstInGroup(Genres); } }

		public string JoinedArtists { get { return JoinGroup(Artists); } }
		public string JoinedPerformers { get { return JoinGroup(Performers); } }
		public string JoinedComposers { get { return JoinGroup(Composers); } }
		public string JoinedGenres { get { return JoinGroup(Genres); } }
		*/
		#endregion
		
		#region Public Properties
		public virtual IList<string> Artists
		{
			get { return artists; }
			set { artists = value; }
		}

		public virtual IList<string> Performers
		{
			get { return performers; }
			set { performers = value; }
		}

		public virtual IList<string> Composers
		{
			get { return composers; }
			set { composers = value; }
		}

		public virtual IList<string> Genres
		{
			get { return genres; }
			set { genres = value; }
		}

		public virtual IList<IPicture> Pictures
		{
			get { return pictures; }
			set { pictures = value; }
		}

		public virtual string Comment
		{
			get { return comment; }
			set { comment = value; }
		}

		public string FirstArtist
		{
			get { return FirstInGroup(Artists); }
		}
		
		public string FirstPerformer
		{
			get { return FirstInGroup(Performers); }
		}
		
		public string FirstComposer
		{
			get { return FirstInGroup(Composers); }
		}
		
		public string FirstGenre
		{
			get { return FirstInGroup(Genres); }
		}

		public string JoinedArtists
		{
			get { return JoinGroup(Artists); }
		}
		
		public string JoinedPerformers
		{
			get { return JoinGroup(Performers); }
		}
		
		public string JoinedComposers
		{
			get { return JoinGroup(Composers); }
		}
		
		public string JoinedGenres
		{
			get { return JoinGroup(Genres); }
		}
		
		[CLSCompliant(false)]
		public virtual uint Year
		{
			get { return year; }
			set { year = value; }
		}

		[CLSCompliant(false)]
		public virtual uint Track
		{
			get { return track; }
			set { track = value; }
		}

		[CLSCompliant(false)]
		public virtual uint TrackCount
		{
			get { return trackCount; }
			set { trackCount = value; }
		}

		[CLSCompliant(false)]
		public virtual uint Disc
		{
			get { return disc; }
			set { disc = value; }
		}

		[CLSCompliant(false)]
		public virtual uint DiscCount
		{
			get { return discCount; }
			set { discCount = value; }
		}
		
		public virtual bool IsEmpty
		{
			get
			{
				return ((Title == null || Title.Length == 0) &&
					(Artists == null || Artists.Count == 0) &&
					(Performers == null || Performers.Count == 0) &&
					(Composers == null || Composers.Count == 0) &&
					(Album == null || Album.Length == 0) &&
					(Comment == null || Comment.Length == 0) &&
					(Genres == null || Genres.Count == 0) &&
					Year == 0 &&
					Track == 0 &&
					TrackCount == 0 &&
					Disc == 0 &&
					DiscCount == 0);
			}
		}
		#endregion
      
		#region Public Static Methods
		public static void Duplicate(Tag source, Tag target, bool overwrite)
		{
			if (overwrite || target.Title == null || target.Title.Length == 0)
				target.Title = source.Title;
			if (overwrite || target.Artists == null || target.Artists.Count == 0)
				target.Artists = source.Artists;
			if (overwrite || target.Performers == null || target.Performers.Count == 0)
				target.Performers = source.Performers;
			if (overwrite || target.Composers == null || target.Composers.Count == 0)
				target.Composers = source.Composers;
			if (overwrite || target.Album == null || target.Album.Length == 0)
				target.Album = source.Album;
			if (overwrite || target.Comment == null || target.Comment.Length == 0)
				target.Comment = source.Comment;
			if (overwrite || target.Genres == null || target.Genres.Count == 0)
				target.Genres = source.Genres;
			if (overwrite || target.Year == 0)
				target.Year = source.Year;
			if (overwrite || target.Track == 0)
				target.Track = source.Track;
			if (overwrite || target.TrackCount == 0)
				target.TrackCount = source.TrackCount;
			if (overwrite || target.Disc == 0)
				target.Disc = source.Disc;
			if (overwrite || target.DiscCount == 0)
				target.DiscCount = source.DiscCount;
		}
		#endregion

		#region IMediaItem Members
		public virtual Guid Id
		{
			get { return id; }
			set { id = value; }
		}
		
		public virtual string Title
		{
			get { return name; }
			set { name = value; }
		}
		
		public virtual string Album
		{
			get { return album; }
			set { album = value; }
		}
		
		public virtual string Artist
		{
			get { return FirstArtist; }
			set { }
		}

		public virtual TimeSpan Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		public virtual DateTime Date
		{
			get
			{
				int year = 1900;
				if (Year > 0)
					year = (int)Year;
				return new DateTime(year, 1, 1);
			}
			set { releaseDate = value; }
		}

		public virtual int Number
		{
			get { return (int)Track; }
			set { Track = (uint)value; }
		}

		public virtual string Format
		{
			get { return format; }
			set { format = value; }
		}
		
		public virtual string Source
		{
			get { return source; }
			set { source = value; }
		}
		
		public virtual string Type
		{
			get { return ModelConstants.MEDIA_TYPE_AUDIO; }
			set { }
		}
		
		public virtual string Status
		{
			get { return null; }
			set { }
		}
		
		[CLSCompliant(false)]
		public IMediaSet Parent
		{
			get { return parent; }
			set { parent = value; }
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
			set { path = value; }
		}

		public virtual string Name
		{
			get { return Title; }
			protected set { Title = value; }
		}
		#endregion
  }
}
