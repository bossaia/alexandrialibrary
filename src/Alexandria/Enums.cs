using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{		
	#region MediaBufferState
	public enum MediaBufferState
	{ 
		None = 0,
		Loading,
		Buffering,
		Ready,
		Starving
	}
	#endregion
	
	#region MediaStreamingState
	public enum MediaStreamingState
	{
		None = 0,
		Connecting,
		Streaming,
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
}
