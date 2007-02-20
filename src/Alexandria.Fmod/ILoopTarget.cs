using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.Fmod
{	
	[CLSCompliant(false)]
	public interface ILoopTarget
	{
		[CLSCompliant(false)]
		SoundLoop Loop{get;set;}
	}
}
