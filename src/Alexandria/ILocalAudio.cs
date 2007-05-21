using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[CLSCompliant(false)]
	public interface ILocalAudio: IAudio, ISeekable, IHasDuration, IHasElapsed, IPositionable
	{
	}
}
