using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISoundCommand
	{
		SoundCommandResult Result{get;}
		bool IsValid(SoundStatus status);
		void Execute(SoundStatus status);
	}
}
