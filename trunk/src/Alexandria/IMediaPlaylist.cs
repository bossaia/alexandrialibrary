using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaPlaylist : IResource
	{
		IList<IMediaContainer> Items { get; }
	}
}
