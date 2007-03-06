using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioCommand : ISoundCommand
	{
		#region Constructors
		protected AudioCommand(AudioCommandType type)
		{
			this.type = type;
		}
		#endregion
	
		#region Private Fields
		private AudioCommandType type = AudioCommandType.None;
		private AudioCommandResult result = AudioCommandResult.None;
		#endregion
	
		#region Public Properties
		public AudioCommandType Type
		{
			get {return type;}
			protected set {type = value;}
		}
		
		public AudioCommandResult Result
		{
			get {return result;}
			protected set {result = value;}
		}
		#endregion
	
		#region Public Methods
		public abstract bool IsValid(AudioStatus status);
		
		public abstract void Execute(AudioStatus status);
		#endregion
	}
}
