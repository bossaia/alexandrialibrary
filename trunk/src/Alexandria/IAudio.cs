using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[CLSCompliant(false)]
	public interface IAudio : IPlayable
	{
		IAudioStatus Status {get;}
	}
}
