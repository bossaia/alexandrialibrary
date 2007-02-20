using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria
{
	public interface ISoundCommand
	{
		SoundCommandResult Result{get;}
		bool IsValid(SoundStatus status);
		void Execute(SoundStatus status);
	}
}
