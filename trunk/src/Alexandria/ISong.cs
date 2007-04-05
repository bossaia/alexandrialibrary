using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISong : IResource
	{
		string Name { get; }
		string Lyrics { get; }
		IList<IArtist> Authors { get; }
	}
}
