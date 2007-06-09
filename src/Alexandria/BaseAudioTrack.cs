using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Data;

namespace Alexandria
{
	public class BaseAudioTrack : BaseMetadata, IAudioTrack
	{
		#region Constructors
		[PersistanceConstructor]
		public BaseAudioTrack(string id, string location, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string format) : this(new Guid(id), new Location(location), name, album, artist, duration, releaseDate, trackNumber, format)
		{
		}
		
		public BaseAudioTrack(Guid id, ILocation location, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string format) : base(id, location, name)
		{
			this.album = album;
			this.artist = artist;
			this.duration = duration;
			this.releaseDate = releaseDate;
			this.trackNumber = trackNumber;
			this.format = format;
		}
		#endregion
		
		#region Private Fields
		private string album;
		private string artist;
		private TimeSpan duration;
		private DateTime releaseDate;
		private int trackNumber;
		private string format;
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
		
		public string Format
		{
			get { return format; }
		}
		#endregion
	}
}
