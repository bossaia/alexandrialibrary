using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IStreamable : IMedia
	{
		float PercentCompleted { get; }
		StreamingState StreamingState { get; }
		void Connect();
		void Disconnect();
	}
}
