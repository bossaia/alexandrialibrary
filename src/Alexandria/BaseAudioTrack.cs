using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class BaseAudioTrack : BaseMetadata, IAudioTrack
	{
		#region Constructors
		public BaseAudioTrack(string alexandriaId, string path, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string format) : this(new Guid(alexandriaId), new Location(path), name, album, artist, duration, releaseDate, trackNumber, format)
		{
		}
		
		public BaseAudioTrack(Guid alexandriaId, ILocation location, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string format) : base(alexandriaId, location, name)
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
