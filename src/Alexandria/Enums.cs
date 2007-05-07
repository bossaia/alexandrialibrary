using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{	
	#region MediaStreamingState
	public enum MediaStreamingState
	{
		None = 0,
		Connecting,
		Streaming,
		Starving
	}
	#endregion
	
	#region MediaPlaybackState
	public enum MediaPlaybackState
	{
		None = 0,
		Playing,
		Paused,
		Stopped
	}
	#endregion
	
	#region MediaSeekingState
	public enum MediaSeekingState
	{
		None,
		Backward,
		Forward
	}
	#endregion
	
	#region AspectRatio
	public enum AspectRatio
	{
		None = 0,
		Native,
		Widescreen,
		AnimorphicWidescreen,
		FullScreen,
		TohoScope,
		Other
	}
	#endregion
}
