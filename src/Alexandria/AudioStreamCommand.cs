using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioStreamCommand : SoundCommand
	{
		#region Constructors
		protected AudioStreamCommand() : base(SoundCommandType.Stream)
		{
		}
		#endregion
	}
}
