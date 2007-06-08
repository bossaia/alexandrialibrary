using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MusicDns
{
	internal class TrackInfo : IAudioTrack
	{
		#region Constructors
		internal TrackInfo()
		{
		}
		#endregion
	
		#region Private Fields
		private string fileName = string.Empty;
		private string fingerprint = string.Empty;
		private string encoding = string.Empty;
		private int bitRate = 0;
		private string format = string.Empty;
		private long lengthInMS = 0;
		private string artist = string.Empty;
		private string track = string.Empty;
		private string album = string.Empty;
		private int trackNum = 0;
		private string genre = string.Empty;
		private string year = string.Empty;
		private string composer = string.Empty;
		private string conductor = string.Empty;
		private string lyricist = string.Empty;
		private string band = string.Empty;
		private string puid = string.Empty;		
		
		private Guid id = Guid.NewGuid();
		private IList<IIdentifier> otherIdentifiers = new List<IIdentifier>();
		ILocation location = null;
		private Version version = new Version(1, 0, 0, 0);
		#endregion
	
		#region Internal Properties
		internal string FileName
		{
			get { return fileName; }
			set
			{
				fileName = value;
				
				if (!string.IsNullOrEmpty(fileName) && location == null)
					location = new Location(fileName);
			}
		}
		
		internal string Fingerprint
		{
			get { return fingerprint; }
			set { fingerprint = value; }
		}
		
		internal string Encoding
		{
			get { return encoding; }
			set { encoding = value; }
		}
		
		internal int BitRate
		{
			get { return bitRate; }
			set { bitRate = value; }
		}

		internal string Format
		{
			get { return format; }
			set { format = value; }
		}

		internal long LengthInMS
		{
			get { return lengthInMS; }
			set { lengthInMS = value; }
		}

		internal string Artist
		{
			get { return artist; }
			set { artist = value; }
		}

		internal string Track
		{
			get { return track; }
			set { track = value; }
		}
		
		internal string Album
		{
			get { return album; }
			set { album = value; }
		}
		
		internal int TrackNum
		{
			get { return trackNum; }
			set { trackNum = value; }
		}
		
		internal string Genre
		{
			get { return genre; }
			set { genre = value; }
		}
		
		internal string Year
		{
			get { return year; }
			set { year = value; }
		}
		
		private string Composer
		{
			get { return composer; }
			set { composer = value; }
		}
		
		private string Conductor
		{
			get { return conductor; }
			set { conductor = value; }
		}
		
		private string Lyricist
		{
			get { return lyricist; }
			set { lyricist = value; }
		}
		
		private string Band
		{
			get { return band; }
			set { band = value; }
		}
		
		internal string Puid
		{
			get { return puid; }
			set
			{
				puid = value;
				
				if (!string.IsNullOrEmpty(puid) && otherIdentifiers.Count == 0)
					otherIdentifiers.Add(new Puid(new Guid(puid)));
			}
		}
		
		internal Version Version
		{
			get { return version; }
			set { version = value; }
		}
		#endregion

		#region IAudioTrack Members
		string IAudioTrack.Album
		{
			get { return album; }
		}

		string IAudioTrack.Artist
		{
			get { return artist; }
		}

		public TimeSpan Duration
		{
			get { return new TimeSpan(0, 0, 0, 0, (int)LengthInMS); }
		}

		public DateTime ReleaseDate
		{
			get
			{
				try
				{
					return Convert.ToDateTime("1/1/" + Year);
				}
				catch (FormatException)
				{
					return DateTime.MinValue;
				}
			}
		}

		public int TrackNumber
		{
			get { return TrackNum; }
		}
		
		string IAudioTrack.Format
		{
			get { return Format; }
		}
		#endregion

		#region IMetadata Members
		public Guid Id
		{
			get { return id; }
		}
		
		public IList<IIdentifier> OtherIdentifiers
		{
			get { return otherIdentifiers; }
		}

		public ILocation Location
		{
			get { return location; }
		}

		public string Name
		{
			get { return Track; }
		}
		#endregion
	}
}
