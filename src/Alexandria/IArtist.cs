using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IArtist : IMetadata
	{
		bool IsGroup { get; }
		DateTime DateStarted { get; }
		DateTime DateStopped { get; }
	}
}
