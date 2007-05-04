using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	public class Album : BaseAlbum
	{
		public Album(string name) : base(Alexandria.Identifier.None, Alexandria.Location.None, name, null, DateTime.MinValue)
		{
		}
		
		public Album(IIdentifier id, ILocation location, string name, IArtist artist, DateTime releaseDate) : base(id, location, name, artist, releaseDate)
		{
		}
	}
}
