using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Resources
{
	/// <summary>
	/// The network state of a media stream
	/// </summary>
	public enum NetworkState
	{
		None = 0,
		Connecting,
		Error,
		Idle,
		Streaming
	}
}
