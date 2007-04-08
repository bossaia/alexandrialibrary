using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISong : IEntity
	{
		string Lyrics { get; }
		IList<IArtist> Authors { get; }
	}
}
