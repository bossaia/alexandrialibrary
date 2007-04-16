using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISong : IMetadata
	{
		string Lyrics { get; }
		IList<IArtist> Authors { get; }
	}
}
