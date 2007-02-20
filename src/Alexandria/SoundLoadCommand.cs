using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public abstract class SoundLoadCommand : SoundCommand
	{
		#region Constructors
		protected SoundLoadCommand() : base(SoundCommandType.Load)
		{
		}
		#endregion
	}
}
