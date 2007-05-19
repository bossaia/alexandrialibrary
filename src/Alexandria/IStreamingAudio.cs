using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IStreamingAudio: IAudio, IStreaming, IHasDuration, IHasElapsed
	{
	}
}
