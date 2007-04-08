using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundUpdateCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundUpdateCommand(Fmod.FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound, MediaCommandType.Update)
		{
		}
		#endregion

		#region Public Methods
		public override bool IsValid(IAudioStatus status)
		{
			return true;
		}

		public override void Execute(IAudioStatus status)
		{
			if (IsValid(status))
			{
				if (Sound.Location.IsLocal)
				{
					//
				}
				else
				{
					Sound.Status.BufferLevel = Convert.ToSingle(Sound.PercentBuffered);
				
					switch (Sound.OpenState)
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
			}
			else Result = MediaCommandResult.InvalidCommand;
		}
		#endregion
	}
}
