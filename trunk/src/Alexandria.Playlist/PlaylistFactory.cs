using System;
using System.Collections.Generic;
using Alexandria.Media;

namespace Alexandria.Playlist
{
	public class PlaylistFactory
	{
		#region Constructors
		public PlaylistFactory()
		{
		}
		#endregion
		
		#region Public Methods
		public IPlaylist CreatePlaylist(Uri path)
		{
			string fileName = path.ToString();
			if (fileName.EndsWith(".m3u", StringComparison.InvariantCultureIgnoreCase))
				return new M3uPlaylist(path);
			else if (fileName.EndsWith(".xspf", StringComparison.InvariantCultureIgnoreCase))
				return new XspPlaylist(path);
			else return null;
		}
		#endregion
	}
}
