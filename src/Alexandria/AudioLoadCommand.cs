using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioLoadCommand : SoundCommand
	{
		#region Constructors
		protected AudioLoadCommand() : base(SoundCommandType.Load)
		{
		}
		#endregion
	}
}
