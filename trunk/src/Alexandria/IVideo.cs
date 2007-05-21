using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[CLSCompliant(false)]
	public interface IVideo : IMedia, IAudible, IPlayable, IVisible
	{		
	}
}
