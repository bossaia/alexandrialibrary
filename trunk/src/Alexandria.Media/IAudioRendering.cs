using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media
{
	public interface IAudioRendering : IMediaRendering
	{
		TimeSpan Duration { get; set; }
	}
}
