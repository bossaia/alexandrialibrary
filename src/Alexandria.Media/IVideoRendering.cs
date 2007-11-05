using System;
using System.Collections.Generic;
using System.Drawing;

namespace Alexandria.Media
{
	public interface IVideoRendering : IMediaRendering
	{
		TimeSpan Duration { get; set; }
		string SizeUnit { get; set; }
		Rectangle Size { get; set; }
	}
}
