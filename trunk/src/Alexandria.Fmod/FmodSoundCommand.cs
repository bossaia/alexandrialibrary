using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace AlexandriaOrg.Alexandria.Fmod
{
	public abstract class FmodSoundCommand : IAudioCommand
	{
		#region Constructors
		protected FmodSoundCommand(FmodAudioPlayer audioPlayer, Fmod.Sound sound, MediaCommandType type)
		{
			this.audioPlayer = audioPlayer;
			this.sound = sound;
			this.type = type;
		}
		#endregion
		
		#region Private Fields
		private MediaCommandType type = MediaCommandType.None;
		private MediaCommandResult result = MediaCommandResult.None;		
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

		public MediaCommandType Type
		{
			get { return type; }
			protected set { type = value; }
		}

		public MediaCommandResult Result
		{
			get { return result; }
			protected set { result = value; }
		}
		#endregion
		
		#region Public Methods
		public abstract bool IsValid(IAudioStatus status);

		public abstract void Execute(IAudioStatus status);
		#endregion
	}
}
