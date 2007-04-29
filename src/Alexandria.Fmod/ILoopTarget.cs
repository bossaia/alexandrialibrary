using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Fmod
{	
	[CLSCompliant(false)]
	public interface ILoopTarget
	{
		[CLSCompliant(false)]
		SoundLoop Loop{get;set;}
	}
}
