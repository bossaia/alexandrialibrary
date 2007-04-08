using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAlbum : IEntity
	{
		DateTime ReleaseDate { get; }
		bool HasVariousArtists { get; }
		IList<IArtist> Performers { get; }
		IList<IAudioTrack> Tracks { get; }
		IList<IGenre> Genres { get; }
		IList<IStyle> Styles { get; }
	}
}
