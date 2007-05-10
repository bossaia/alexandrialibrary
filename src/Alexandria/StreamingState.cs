using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	/// <summary>
	/// The streaming state of a media resource
	/// </summary>
	public enum StreamingState
	{
		None = 0,
		Connecting,
		Streaming,
		Starving
	}
}
