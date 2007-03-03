using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMediaController
	{
		Dictionary<int, IMediaFile> Items{get;}
	}
}
