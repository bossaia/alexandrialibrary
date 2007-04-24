using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseAlbum : BaseMetadata, IAlbum
	{
		#region Constructors
		public BaseAlbum(IIdentifier id, ILocation location, string name, DateTime releaseDate, bool hasVariousArtists) : base(id, location, name)
		{
			this.releaseDate = releaseDate;
			this.hasVariousArtists = hasVariousArtists;
		}
		#endregion
		
		#region Private Fields
		private DateTime releaseDate;
		private bool hasVariousArtists;
		private List<IArtist> performers = new List<IArtist>();
		private List<IAudioTrack> tracks = new List<IAudioTrack>();
		private List<IGenre> genres = new List<IGenre>();
		private List<IStyle> styles = new List<IStyle>();
		#endregion
	
		#region IAlbum Members
		public DateTime ReleaseDate
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool HasVariousArtists
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IArtist> Performers
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IAudioTrack> Tracks
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IGenre> Genres
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public IList<IStyle> Styles
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		#endregion
	}
}
