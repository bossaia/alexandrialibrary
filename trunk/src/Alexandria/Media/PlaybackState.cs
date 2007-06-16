using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	/// <summary>
	/// The playback state of a media resource
	/// </summary>
	public enum PlaybackState
	{
		None = 0,
		Playing,
		Paused,
		Stopped
	}
}
