using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class PlsPlaylistFormatter : PlaylistFormatter
	{
		public override Playlist LoadPlaylistFromFile(string fileName)
		{
			Playlist playlist = null;
			if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
			{
				playlist = new Playlist();
			}

			return playlist;
		}

		public override void SavePlaylistToFile(Playlist playlist, string fileName)
		{
			if (playlist != null && !string.IsNullOrEmpty(fileName))
			{
				StreamWriter writer = new StreamWriter(fileName);
				//writer.WriteLine();
			}
		}
	}
}
