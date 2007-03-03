using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class SoundCommand : ISoundCommand
	{
		#region Constructors
		protected SoundCommand(SoundCommandType type)
		{
			this.type = type;
		}
		#endregion
	
		#region Private Fields
		private SoundCommandType type = SoundCommandType.None;
		private SoundCommandResult result = SoundCommandResult.None;
		#endregion
	
		#region Public Properties
		public SoundCommandType Type
		{
			get {return type;}
			protected set {type = value;}
		}
		
		public SoundCommandResult Result
		{
			get {return result;}
			protected set {result = value;}
		}
		#endregion
	
		#region Public Methods
		public abstract bool IsValid(SoundStatus status);
		
		public abstract void Execute(SoundStatus status);
		#endregion
	}
}
