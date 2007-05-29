using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack : IMetadata
	{
		string Album { get; }
		string Artist { get; }
		TimeSpan Duration { get; }
		DateTime ReleaseDate { get; }
		int TrackNumber { get; }
		string Format { get; }
	}
}
