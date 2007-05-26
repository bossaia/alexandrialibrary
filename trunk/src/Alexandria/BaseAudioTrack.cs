using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseAudioTrack : BaseMetadata, IAudioTrack
	{
		#region Constructors
		public BaseAudioTrack(IIdentifier id, ILocation location, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber) : base(id, location, name)
		{
			this.album = album;
			this.artist = artist;
			this.duration = duration;
			this.releaseDate = releaseDate;
			this.trackNumber = trackNumber;
		}
		#endregion
		
		#region Private Fields
		private string album;
		private string artist;
		private TimeSpan duration;
		private DateTime releaseDate;
		private int trackNumber;
		#endregion
	
		#region IAudioTrack Members
		public string Album
		{
			get { return album; }
		}

		public string Artist
		{
			get { return artist; }
		}

		public TimeSpan Duration
		{
			get { return duration; }
		}

		public DateTime ReleaseDate
		{
			get { return releaseDate; }
		}
		
		public int TrackNumber
		{
			get { return trackNumber; }
		}
		#endregion
	}
}
