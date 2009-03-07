using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public abstract class PlaylistFormatter
	{
		public abstract Playlist LoadPlaylistFromFile(string fileName);
		public abstract void SavePlaylistToFile(Playlist playlist, string fileName);
	}
}
