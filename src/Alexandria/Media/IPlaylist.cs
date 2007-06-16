using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlaylist : IMedia
	{
		IList<IPlaylistItem> Items { get; }
	}
}
