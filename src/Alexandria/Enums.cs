using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{		
	#region MediaBufferState
	public enum MediaBufferState
	{ 
		None = 0,
		Connecting,
		Loading,
		Buffering,
		Ready,
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
		
	#region MediaCommandType
	public enum MediaCommandType
	{
		None = 0,
		Load,
		Stream,
		Play,
		Pause,
		Stop,
		Seek,
		Update
	}
	#endregion
	
	#region MediaCommandResult
	public enum MediaCommandResult
	{
		/// <summary>
		/// There was no result from this Update
		/// </summary>
		None = 0,
		/// <summary>
		/// The given command was not valid for the current sound status
		/// </summary>
		InvalidCommand,
		/// <summary>
		/// The audio resource has been loaded successfully
		/// </summary>
		Loaded,
		/// <summary>
		/// The was an error loading the audio resource
		/// </summary>
		LoadError,
		/// <summary>
		/// The audio resource is streaming successfully
		/// </summary>
		Streaming,
		/// <summary>
		/// There was an error streaming the audio resource
		/// </summary>
		StreamError
	}
	#endregion
}
