using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public abstract class SoundSeekCommand : SoundCommand
	{
		#region Constructors
		protected SoundSeekCommand() : base(SoundCommandType.Seek)
		{
		}
		#endregion
	}
}
