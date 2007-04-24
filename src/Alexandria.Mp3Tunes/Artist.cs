using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	internal class Artist : BaseArtist
	{
		public Artist(IIdentifier id, ILocation location, string name, bool isGroup, DateTime dateStarted, DateTime dateStopped) : base(id, location, name, isGroup, dateStarted, dateStopped)
		{
		}
	}
}
