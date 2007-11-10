using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Resources
{
	/// <summary>
	/// The playback state of a media stream
	/// </summary>
	public enum PlaybackState
	{
		None = 0,
		Error,
		Playing,
		Paused,
		Stopped
	}
}
