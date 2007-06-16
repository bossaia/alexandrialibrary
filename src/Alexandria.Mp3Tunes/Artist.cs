using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Data;

namespace Alexandria.Mp3Tunes
{
	internal class Artist : BaseArtist
	{
		public Artist(Guid alexandriaId, ILocation location, string name) : base(alexandriaId, location, name)
		{
		}
	}
}
