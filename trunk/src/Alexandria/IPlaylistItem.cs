using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlaylistItem
	{
		ILocation Location { get;  }
		IDataMap Map { get; }
	}
}
