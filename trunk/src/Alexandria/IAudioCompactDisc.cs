using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioCompactDisc : IAudio
	{
		IList<IAudio> Tracks {get;}
	}
}
