using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseAudioTrack : BaseMetadata, IAudioTrack
	{
		#region Constructors
		public BaseAudioTrack(IIdentifier id, ILocation location, string name, int number, TimeSpan length, DateTime releaseDate, IAlbum album, ISong song) : base(id, location, name)
		{
			this.number = number;
			this.length = length;
			this.releaseDate = releaseDate;
			this.album = album;
			this.song = song;
		}
		#endregion
		
		#region Private Fields
		private int number;
		private TimeSpan length;
		private DateTime releaseDate;
		private IAlbum album;
		private ISong song;
		private List<IArtist> performers = new List<IArtist>();
		private List<IGenre> genres = new List<IGenre>();
		private List<IStyle> styles = new List<IStyle>();
		#endregion
	
		#region IAudioTrack Members
		public int Number
		{
			get { return number; }
		}

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

		public IList<IArtist> Performers
		{
			get { return performers; }
		}

		public IList<IGenre> Genres
		{
			get { return genres; }
		}

		public IList<IStyle> Styles
		{
			get { return styles; }
		}
		#endregion
	}
}
