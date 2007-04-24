using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack : IMetadata
	{
		int Number { get; }
		TimeSpan Length { get; }
		DateTime ReleaseDate { get; }
		IAlbum Album { get; }
		ISong Song { get; }
		IList<IArtist> Performers { get; }
		IList<IGenre> Genres { get; }
		IList<IStyle> Styles { get; }
	}
}
