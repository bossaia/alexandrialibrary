using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioTrack : IResource
	{
		int Number { get; }
		string Name { get; }
		ISong Song { get; }
		IList<IArtist> Performers { get; }
	}
}
