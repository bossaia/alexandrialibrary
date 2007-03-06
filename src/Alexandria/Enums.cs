using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{		
	#region AudioBufferState
	public enum AudioBufferState
	{
		None = 0,
		Loading,
		Connecting,
		Buffering,
		Ready,
		Starving
	}
	#endregion
	
	#region AudioPlaybackState
	public enum AudioPlaybackState
	{
		None = 0,
		Playing,
		Paused,
		Stopped
	}
	#endregion
		
	#region AudioCommandType
	public enum AudioCommandType
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
	
	#region AudioCommandResult
	public enum AudioCommandResult
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
