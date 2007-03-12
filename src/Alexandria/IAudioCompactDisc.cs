using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioCompactDisc : IAudioResource
	{
		IList<IAudioResource> Tracks {get;}
	}
}
