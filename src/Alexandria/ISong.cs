using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISong : INamedResource
	{
		string Lyrics { get; }
		IList<IArtist> Authors { get; }
	}
}
