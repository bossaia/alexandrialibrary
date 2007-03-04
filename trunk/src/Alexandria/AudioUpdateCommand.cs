using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class AudioUpdateCommand : SoundCommand
	{
		#region Constructors
		public AudioUpdateCommand() : base(SoundCommandType.Update)
		{
		}
		#endregion
	}
}
