using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IStreamable : IMedia
	{
		float PercentCompleted { get; }
		MediaStreamingState StreamingState { get; }
		void Connect();
		void Disconnect();
	}
}
