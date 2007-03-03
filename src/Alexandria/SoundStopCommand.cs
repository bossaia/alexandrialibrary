using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class SoundStopCommand : SoundCommand
	{
		#region Constructors
		protected SoundStopCommand() : base(SoundCommandType.Stop)
		{
		}
		#endregion
	}
}
