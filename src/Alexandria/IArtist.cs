using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IArtist : IEntity
	{
		bool IsGroup { get; }
		DateTime DateStarted { get; }
		DateTime DateStopped { get; }
	}
}
