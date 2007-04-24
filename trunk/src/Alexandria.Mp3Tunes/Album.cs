using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	public class Album : BaseAlbum
	{
		public Album(IIdentifier id, ILocation location, string name, DateTime releaseDate, bool hasVariousArtists) : base(id, location, name, releaseDate, hasVariousArtists)
		{
		}
	}
}
