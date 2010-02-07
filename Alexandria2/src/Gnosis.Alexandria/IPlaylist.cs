using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IPlaylist
		: INamed, IEquatable<IPlaylist>
	{
		ITuple<IPlaylistItem> Items { get; }

		void AddItem(IPlaylistItem item);
		void RemoveItem(IPlaylistItem item);
	}
}
