using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioCompactDisc : IResource
	{
		int NumberOfTracks { get; }
		int Minutes { get; }
		int Seconds { get; }
		int Frames { get; }
	}
}
