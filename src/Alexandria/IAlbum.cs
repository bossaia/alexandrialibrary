using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAlbum : IResource
	{
		string Name { get; }
		DateTime ReleaseDate { get; }
		bool HasVariousArtists { get; }
		IArtist PrimaryArtist { get; }
		IList<IArtist> Artists { get; }
		IList<IAudioTrack> Tracks { get; }
	}
}
