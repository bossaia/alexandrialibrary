using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioPauseCommand : SoundCommand
	{
		#region Constructors
		protected AudioPauseCommand() : base(SoundCommandType.Pause)
		{
		}
		#endregion
	}
}
