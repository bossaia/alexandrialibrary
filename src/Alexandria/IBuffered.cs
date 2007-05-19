using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IBuffered
	{
		float PercentBuffered { get; }
		BufferState BufferState { get; }
	}
}
