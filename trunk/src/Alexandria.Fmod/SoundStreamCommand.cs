using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundStreamCommand : FmodSoundCommand
	{
		#region Constructors
		[CLSCompliant(false)]
		public SoundStreamCommand(FmodAudioPlayer audioPlayer, Fmod.Sound sound, uint streamBufferSize) : base(audioPlayer, sound, MediaCommandType.Stream)
		{
			this.streamBufferSize = streamBufferSize;
		}
		#endregion
		
		#region Private Fields
		private uint streamBufferSize;
		#endregion
		
		#region Public Properties
		[CLSCompliant(false)]
		public uint StreamBufferSize
		{
			get {return streamBufferSize;}
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(IAudioStatus status)
		{
			//if (status != null)
				//return status.AllowsStream;
			//else
				return false;
		}

		public override void Execute(IAudioStatus status)
		{
			if (IsValid(status))
			{
				Sound.Load(streamBufferSize);
				if (AudioPlayer.SoundSystem.CurrentResult == Fmod.Result.Ok)
				{
					switch(Sound.OpenState)
					{
						case OpenState.Error:
							Sound.Status.BufferState = MediaBufferState.None;
							Sound.Status.PlaybackState = MediaPlaybackState.None;
							Sound.Status.SeekingState = MediaSeekingState.None;
							Result = MediaCommandResult.StreamError;
							break;
						case OpenState.Buffering:
							Sound.Status.BufferState = MediaBufferState.Buffering;
							break;
						case OpenState.Connecting:
							Sound.Status.StreamingState = MediaStreamingState.Connecting;
							break;
						case OpenState.Loading:
							Sound.Status.BufferState = MediaBufferState.Loading;
							break;
						case OpenState.Ready:
							Result = MediaCommandResult.Streaming;
							Sound.Status = RemoteSoundReady.Example;
							break;
						default:
							Sound.Status.BufferState = MediaBufferState.None;
							Sound.Status.PlaybackState = MediaPlaybackState.None;
							Sound.Status.SeekingState = MediaSeekingState.None;
							Result = MediaCommandResult.StreamError;
							break;
					}
				}
				else Result = MediaCommandResult.StreamError;
			}
			else Result = MediaCommandResult.InvalidCommand;
		}
		#endregion
	}
}
