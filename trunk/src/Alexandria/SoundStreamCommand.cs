using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class SoundStreamCommand : SoundCommand
	{
		#region Constructors
		protected SoundStreamCommand() : base(SoundCommandType.Stream)
		{
		}
		#endregion
	}
}
