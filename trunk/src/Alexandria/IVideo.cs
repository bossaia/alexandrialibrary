using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IVideo : IPlayable
	{
		IVideoStatus Status { get; }
	}
}
