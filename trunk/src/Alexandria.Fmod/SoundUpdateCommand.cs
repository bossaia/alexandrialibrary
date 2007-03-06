using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundUpdateCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundUpdateCommand(Fmod.FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound, AudioCommandType.Update)
		{
		}
		#endregion

		#region Public Methods
		public override bool IsValid(AudioStatus status)
		{
			return true;
		}

		public override void Execute(AudioStatus status)
		{
			if (IsValid(status))
			{
				if (Sound.MediaFile.IsLocal)
				{
					//
				}
				else
				{
					Sound.Status.BufferLevel = Convert.ToSingle(Sound.PercentBuffered);
				
					switch (Sound.OpenState)
					{
						case OpenState.Error:
							Sound.Status.BufferState = AudioBufferState.None;
							Sound.Status.PlaybackState = AudioPlaybackState.None;
							Sound.Status.IsSeeking = false;
							Result = AudioCommandResult.StreamError;
							break;
						case OpenState.Buffering:
							Sound.Status.BufferState = AudioBufferState.Buffering;
							break;
						case OpenState.Connecting:
							Sound.Status.BufferState = AudioBufferState.Connecting;
							break;
						case OpenState.Loading:
							Sound.Status.BufferState = AudioBufferState.Loading;
							break;
						case OpenState.Ready:
							Result = AudioCommandResult.Streaming;
							Sound.Status = RemoteSoundReady.Example;
							break;
						default:
							Sound.Status.BufferState = AudioBufferState.None;
							Sound.Status.PlaybackState = AudioPlaybackState.None;
							Sound.Status.IsSeeking = false;
							Result = AudioCommandResult.StreamError;
							break;
					}
				}
			}
			else Result = AudioCommandResult.InvalidCommand;
		}
		#endregion
	}
}
