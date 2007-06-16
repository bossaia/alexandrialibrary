using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IHasDuration
	{
		TimeSpan Duration { get; }
	}
}
