using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioSeekCommand : SoundCommand
	{
		#region Constructors
		protected AudioSeekCommand() : base(SoundCommandType.Seek)
		{
		}
		#endregion
	}
}
