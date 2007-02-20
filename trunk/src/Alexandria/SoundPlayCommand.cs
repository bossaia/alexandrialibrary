using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public abstract class SoundPlayCommand : SoundCommand
	{
		#region Constructors
		protected SoundPlayCommand() : base(SoundCommandType.Play)
		{
		}
		#endregion
	}
}
