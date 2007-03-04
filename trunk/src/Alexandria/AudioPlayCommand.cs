using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioPlayCommand : SoundCommand
	{
		#region Constructors
		protected AudioPlayCommand() : base(SoundCommandType.Play)
		{
		}
		#endregion
	}
}
