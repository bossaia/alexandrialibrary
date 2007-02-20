using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundPlayCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundPlayCommand(Fmod.FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound, SoundCommandType.Play)
		{
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(SoundStatus status)
		{
			if (status != null)
				return status.AllowsPlay;
			else
				return false;
		}

		public override void Execute(SoundStatus status)
		{
			if (IsValid(status))
			{
				if (Sound.MediaFile.IsLocal)
				{
					//AudioPlayer.Play(
				}
				else
				{
				
				}
			}
			else Result = SoundCommandResult.InvalidCommand;
		}
		#endregion
	}
}
