using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAlbum : IMetadata
	{		
		string Artist { get; }
		DateTime ReleaseDate { get; }
		IList<IAudioTrack> Tracks { get; }
	}
}
