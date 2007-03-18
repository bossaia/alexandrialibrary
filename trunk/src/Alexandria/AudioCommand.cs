using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioCommand : IAudioCommand
	{
		#region Constructors
		protected AudioCommand(MediaCommandType type)
		{
			this.type = type;
		}
		#endregion
	
		#region Private Fields
		private MediaCommandType type = MediaCommandType.None;
		private MediaCommandResult result = MediaCommandResult.None;
		#endregion
	
		#region Public Properties
		public MediaCommandType Type
		{
			get {return type;}
			protected set {type = value;}
		}
		
		public MediaCommandResult Result
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
