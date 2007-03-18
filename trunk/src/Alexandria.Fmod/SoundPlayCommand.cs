using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundPlayCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundPlayCommand(Fmod.FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound, MediaCommandType.Play)
		{
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(AudioStatus status)
		{
			if (status != null)
				return status.AllowsPlay;
			else
				return false;
		}

		public override void Execute(AudioStatus status)
		{
			if (IsValid(status))
			{
				if (Sound.IsLocal)
				{
					//AudioPlayer.Play(
				}
				else
				{
				
				}
			}
			else Result = MediaCommandResult.InvalidCommand;
		}
		#endregion
	}
}
