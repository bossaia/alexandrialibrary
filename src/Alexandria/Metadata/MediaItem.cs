using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class MediaItem : IMediaItem
	{
		#region Constructors
		public MediaItem(Guid id, string source, string type, int number, string title, string artist, string album, TimeSpan duration, DateTime date, string format, Uri path)
		{
			this.id = id;
			this.source = source;
			this.type = type;
			this.number = number;
			this.title = title;
			this.artist = artist;
			this.album = album;
			this.duration = duration;
			this.date = date;
			this.format = format;
			this.path = path;
		}
		#endregion
	
		#region Private Fields
		private Guid id;
		private string status = string.Empty;
		private string source;
		private string type;
		private int number;
		private string title;
		private string artist;
		private string album;
		private TimeSpan duration;
		private DateTime date;
		private string format;
		private Uri path;
		#endregion
	
		#region IMediaItem Members
		public Guid Id
		{
			get { return id; }
			set { id = value; }
		}
		
		public string Status
		{
			get { return status; }
			set { status = value; }
		}
		
		public string Source
		{
			get { return source; }
			set { source = value; }
		}

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public int Number
		{
			get { return number; }
			set { number = value; }
		}

		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		public string Artist
		{
			get { return artist; }
			set { artist = value; }
		}

		public string Album
		{
			get { return album; }
			set { album = value; }
		}

		public TimeSpan Duration
		{
			get { return duration; }
			set { duration = value; }
		}

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		public string Format
		{
			get { return format; }
			set { format = value; }
		}

		public Uri Path
		{
			get { return path; }
			set { path = value; }
		}
		#endregion
	}
}
