using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[CLSCompliant(false)]
	public interface IStreamingAudio: IAudio, IStreaming, IHasDuration, IHasElapsed
	{
	}
}
