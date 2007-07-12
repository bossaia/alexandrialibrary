using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Persistence;

namespace Alexandria.Metadata
{
	[Record("Track", typeof(IAudioTrack))]
	[RecordType(typeof(IAudioTrack))]
	public class ProxyAudioTrack : IAudioTrack
	{
		#region Constructors
		[Factory(typeof(IAudioTrack))]
		public ProxyAudioTrack(Guid id, Uri path, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string format)
		{
			this.id = id;
			this.path = path;
			this.name = name;
			this.album = album;
			this.artist = artist;
			this.duration = duration;
			this.releaseDate = releaseDate;
			this.trackNumber = trackNumber;
			this.format = format;
		}
		#endregion
	
		#region Private Fields
		private Guid id;
		private IRecord parent;
		private IList<IMetadataIdentifier> metadataIdentifiers = new List<IMetadataIdentifier>();
		private Uri path;
		private string name;
		
		private string album;
		private string artist;
		private TimeSpan duration;
		private DateTime releaseDate;
		private int trackNumber;
		private string format;
		
		private IPersistenceBroker broker;
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

		#region IMetadata Members
		public IList<IMetadataIdentifier> MetadataIdentifiers
		{
			get { return metadataIdentifiers; }
		}

		public Uri Path
		{
			get { return path; }
		}

		public string Name
		{
			get { return name; }
		}
		#endregion

		#region IRecord Members
		public Guid Id
		{
			get { return id; }
		}

		public IRecord Parent
		{
			get { return parent; }
			set { parent = value; }
		}

		public IPersistenceBroker PersistenceBroker
		{
			get { return broker; }
			set { broker = value; }
		}

		public bool IsProxy
		{
			get { return true; }
		}

		public void Save()
		{
			broker.SaveRecord(this);
		}

		public void Delete()
		{
			broker.DeleteRecord(this);
		}
		#endregion
	}
}
