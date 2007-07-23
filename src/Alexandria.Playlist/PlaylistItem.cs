using System;
using System.Collections.Generic;
using Alexandria.Media;

namespace Alexandria.Playlist
{
	public class PlaylistItem : IPlaylistItem
	{
		#region Constructors
		public PlaylistItem(Uri path)
		{
			this.path = path;
		}
		
		public PlaylistItem(Uri path, TimeSpan duration) : this(path)
		{
			this.duration = duration;
		}
		
		public PlaylistItem(Uri path, string name) : this(path)
		{
			this.name = name;
		}
		
		public PlaylistItem(Uri path, TimeSpan duration, string name) : this(path, duration)
		{
			this.name = name;
		}
		#endregion
	
		#region Private Fields
		private Uri path;
		private TimeSpan duration = TimeSpan.Zero;
		private string name;
		#endregion
	
		#region IPlaylistItem Members
		public string Name
		{
			get { return name; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public TimeSpan Duration
		{
			get { return duration; }
		}
		#endregion
	}
}
