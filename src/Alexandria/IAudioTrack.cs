using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack : IResource
	{
		int Number { get; }
		string Name { get; }
		IList<IArtist> Artists { get; }
	}
}
