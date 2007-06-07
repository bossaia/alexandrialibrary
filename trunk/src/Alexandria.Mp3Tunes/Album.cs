using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	public class Album : BaseAlbum
	{
		public Album(string name) : base(Guid.NewGuid(), Alexandria.Location.None, name, null, DateTime.MinValue)
		{
		}

		public Album(string name, string artist) : base(Guid.NewGuid(), Alexandria.Location.None, name, artist, DateTime.MinValue)
		{
		}

		public Album(ILocation location, string name, string artist, DateTime releaseDate) : base(Guid.NewGuid(), location, name, artist, releaseDate)
		{
		}
		
		public Album(Guid alexandriaId, ILocation location, string name, string artist, DateTime releaseDate) : base(alexandriaId, location, name, artist, releaseDate)
		{
		}
	}
}
