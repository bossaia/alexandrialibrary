using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioCommand
	{
		MediaCommandResult Result{get;}
		bool IsValid(AudioStatus status);
		void Execute(AudioStatus status);
	}
}
