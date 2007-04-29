using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.Fmod
{
	public class SoundPlayCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundPlayCommand(Fmod.FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound)
		{
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(IAudioStatus status)
		{
			//if (status != null)
				//return status.AllowsPlay;
			//else
				return false;
		}

		public override void Execute(IAudioStatus status)
		{
			if (IsValid(status))
			{
				if (Sound.Location.IsLocal)
				{
					//AudioPlayer.Play(
				}
				else
				{
				
				}
			}
			//else Result = MediaCommandResult.InvalidCommand;
		}
		#endregion
	}
}
