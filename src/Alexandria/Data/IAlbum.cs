using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Data
{
	public interface IAlbum : IMetadata
	{		
		string Artist { get; }
		DateTime ReleaseDate { get; }
		IList<IAudioTrack> Tracks { get; }
	}
}
