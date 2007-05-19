using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IStreamable
	{
		float PercentCompleted { get; }
		StreamingState StreamingState { get; }
	}
}
