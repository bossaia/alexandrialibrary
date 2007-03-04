using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioStopCommand : SoundCommand
	{
		#region Constructors
		protected AudioStopCommand() : base(SoundCommandType.Stop)
		{
		}
		#endregion
	}
}
