using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundUpdateCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundUpdateCommand(Fmod.FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound, SoundCommandType.Update)
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
							Sound.Status.BufferState = BufferState.None;
							Sound.Status.PlaybackState = PlaybackState.None;
							Sound.Status.SeekState = SeekState.None;
							Result = SoundCommandResult.SoundStreamError;
							break;
						case OpenState.Buffering:
							Sound.Status.BufferState = BufferState.Buffering;
							break;
						case OpenState.Connecting:
							Sound.Status.BufferState = BufferState.Connecting;
							break;
						case OpenState.Loading:
							Sound.Status.BufferState = BufferState.Loading;
							break;
						case OpenState.Ready:
							Result = SoundCommandResult.SoundStreaming;
							Sound.Status = RemoteSoundReady.Example;
							break;
						default:
							Sound.Status.BufferState = BufferState.None;
							Sound.Status.PlaybackState = PlaybackState.None;
							Sound.Status.SeekState = SeekState.None;
							Result = SoundCommandResult.SoundStreamError;
							break;
					}
				}
			}
			else Result = SoundCommandResult.InvalidCommand;
		}
		#endregion
	}
}
