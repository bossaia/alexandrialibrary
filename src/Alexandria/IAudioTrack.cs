using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack : IMetadata
	{
		int Number { get; }
		TimeSpan Length { get; }
		ISong Song { get; }
		IList<IArtist> Performers { get; }
		IList<IGenre> Genres { get; }
		IList<IStyle> Styles { get; }
	}
}
