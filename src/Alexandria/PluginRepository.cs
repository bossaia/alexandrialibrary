using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public static class PluginRepository
	{
		#region Private Static Fields
		private static List<IAlbumFactory> AlbumFactories = new List<IAlbumFactory>();
		private static List<IArtistFactory> ArtistFactories = new List<IArtistFactory>();
		#endregion
	}
}
