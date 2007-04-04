using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack
	{
		int Number { get; }
		string Name { get; }
		IList<IArtist> Artists { get; }
		IAudio Audio { get; }
	}
}
