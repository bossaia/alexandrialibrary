using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundLoadCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundLoadCommand(FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound, SoundCommandType.Load)
		{
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(AudioStatus status)
		{
			if (status != null)
				return status.AllowsLoad;
			else
				return false;
		}
		
		public override void Execute(AudioStatus status)
		{
			if (IsValid(status))
			{				
				Sound.Load();
				if (AudioPlayer.SoundSystem.CurrentResult == Fmod.Result.Ok)
				{
					Result = SoundCommandResult.SoundLoaded;
					Sound.Status = LocalSoundLoaded.Example;
				}
				else Result = SoundCommandResult.SoundLoadError;
			}
			else Result = SoundCommandResult.InvalidCommand;
		}
		#endregion
	}
}
