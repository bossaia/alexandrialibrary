using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public abstract class SoundPauseCommand : SoundCommand
	{
		#region Constructors
		protected SoundPauseCommand() : base(SoundCommandType.Pause)
		{
		}
		#endregion
	}
}
