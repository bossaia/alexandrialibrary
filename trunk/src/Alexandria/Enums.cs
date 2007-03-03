using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{	
	#region SeekDirection
	public enum SeekDirection
	{
		Backward = 0,
		Forward = 1
	}
	#endregion
	
	#region MediaTypes
	[Flags]
	public enum MediaTypes
	{
		None = 0x000,
		Audio = 0x001,
		Video = 0x002,
		Image = 0x004
	}
	#endregion
	
	#region FileExtension
	public enum FileExtension
	{
		None = 0,
		Ogg,
		Ogm,
		Mp3,
		Wav,
		Flac,
		Wma,
		M4a,
		Jpg,
		Jpeg,
		Pcm
	}
	#endregion
	
	#region BufferState
	public enum BufferState
	{
		None = 0,
		Loading,
		Connecting,
		Buffering,
		Ready,
		Starving
	}
	#endregion
	
	#region PlaybackState
	public enum PlaybackState
	{
		None = 0,
		Playing,
		Paused,
		Stopped
	}
	#endregion
	
	#region SeekState
	public enum SeekState
	{
		None = 0,
		Seeking
	}
	#endregion
	
	#region SoundCommandType
	public enum SoundCommandType
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
	
	#region SoundCommandResult
	public enum SoundCommandResult
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
		/// The sound has been loaded successfully
		/// </summary>
		SoundLoaded,
		/// <summary>
		/// The was an error loading the sound
		/// </summary>
		SoundLoadError,
		/// <summary>
		/// The sound is streaming successfully
		/// </summary>
		SoundStreaming,
		/// <summary>
		/// There was an error streaming the sound
		/// </summary>
		SoundStreamError
	}
	#endregion

	public enum ContentType
	{
		Application,
		Audio,
		Image,
		Message,
		Model,
		Multipart,
		Text,
		Video,
	}
}
