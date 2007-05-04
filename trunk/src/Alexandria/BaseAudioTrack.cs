using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseAudioTrack : BaseMetadata, IAudioTrack
	{
		#region Constructors
		public BaseAudioTrack(IIdentifier id, ILocation location, string name, TimeSpan length, DateTime releaseDate, IAlbum album, IArtist artist, ISong song) : base(id, location, name)
		{			
			this.length = length;
			this.releaseDate = releaseDate;
			this.album = album;
			this.artist = artist;
			this.song = song;
		}
		#endregion
		
		#region Private Fields
		private TimeSpan length;
		private DateTime releaseDate;
		private IAlbum album;
		private IArtist artist;
		private ISong song;
		#endregion
	
		#region IAudioTrack Members
		public TimeSpan Length
		{
			get { return length; }
		}

		public DateTime ReleaseDate
		{
			get { return releaseDate; }
		}

		public IAlbum Album
		{
			get { return album; }
		}

		public ISong Song
		{
			get { return song; }
		}

		public IArtist Artist
		{
			get { return artist; }
		}
		#endregion
	}
}
