using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class BaseAlbum : BaseMetadata, IAlbum
	{
		#region Constructors
		public BaseAlbum(IIdentifier id, ILocation location, string name, IArtist artist, DateTime releaseDate) : base(id, location, name)
		{
			this.artist = artist;
			this.releaseDate = releaseDate;
		}
		#endregion
		
		#region Private Fields
		private IArtist artist;
		private DateTime releaseDate = DateTime.MinValue;
		private List<IAudioTrack> tracks = new List<IAudioTrack>();
		#endregion
	
		#region IAlbum Members
		/// <summary>
		/// Get the Artist credited with this album
		/// </summary>
		public IArtist Artist
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		
		/// <summary>
		/// Get the earliest release date of this album
		/// </summary>
		public DateTime ReleaseDate
		{
			get { return releaseDate; }
		}
		
		/// <summary>
		/// Get the tracks on this album
		/// </summary>
		public IList<IAudioTrack> Tracks
		{
			get { return tracks; }
		}
		#endregion
	}
}
