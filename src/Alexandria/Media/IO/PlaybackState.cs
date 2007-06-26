using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	/// <summary>
	/// The playback state of a media stream
	/// </summary>
	public enum PlaybackState
	{
		None = 0,
		Playing,
		Paused,
		Stopped
	}
}
