using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ILocalAudio: IAudio, ISeekable, IHasDuration, IHasElapsed, IPositionable
	{
	}
}
