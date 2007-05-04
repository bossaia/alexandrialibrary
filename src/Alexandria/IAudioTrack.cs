using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack : IMetadata
	{	
		TimeSpan Length { get; }
		DateTime ReleaseDate { get; }
		IAlbum Album { get; }
		IArtist Artist { get; }
		ISong Song { get; }
	}
}
