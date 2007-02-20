using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public abstract class SoundUpdateCommand : SoundCommand
	{
		#region Constructors
		public SoundUpdateCommand() : base(SoundCommandType.Update)
		{
		}
		#endregion
	}
}
