using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public abstract class FmodSoundCommand : AlexandriaOrg.Alexandria.SoundCommand
	{
		#region Constructors
		protected FmodSoundCommand(FmodAudioPlayer audioPlayer, Fmod.Sound sound, SoundCommandType type) : base(type)
		{
			this.audioPlayer = audioPlayer;
			this.sound = sound;
		}
		#endregion
		
		#region Private Fields
		private FmodAudioPlayer audioPlayer;
		private Fmod.Sound sound;
		#endregion
		
		#region Public Properties
		public FmodAudioPlayer AudioPlayer
		{
			get {return audioPlayer;}
		}
		
		public Fmod.Sound Sound
		{
			get {return sound;}
		}
		#endregion
	}
}
