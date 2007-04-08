using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioCompactDisc : IResource
	{		
		int Minutes { get; }
		int Seconds { get; }
		int Frames { get; }
		IList<IAudio> Tracks { get; }
	}
}
