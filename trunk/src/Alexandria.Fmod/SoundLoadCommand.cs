using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public class SoundLoadCommand : FmodSoundCommand
	{
		#region Constructors
		public SoundLoadCommand(FmodAudioPlayer audioPlayer, Fmod.Sound sound) : base(audioPlayer, sound)
		{
		}
		#endregion
		
		#region Public Methods
		public override bool IsValid(IAudioStatus status)
		{
			//if (status != null)
				//return status.AllowsLoad;
			//else
				return false;
		}
		
		public override void Execute(IAudioStatus status)
		{
			if (IsValid(status))
			{				
				Sound.Load();
				if (AudioPlayer.SoundSystem.CurrentResult == Fmod.Result.Ok)
				{
					//Result = MediaCommandResult.Loaded;
					Sound.Status = LocalSoundLoaded.Example;
				}
				//else Result = MediaCommandResult.LoadError;
			}
			//else Result = MediaCommandResult.InvalidCommand;
		}
		#endregion
	}
}
