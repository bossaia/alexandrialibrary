using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IArtistFactory : IPlugin
	{
		IArtist GetArtist(ISearch search);
	}
}
