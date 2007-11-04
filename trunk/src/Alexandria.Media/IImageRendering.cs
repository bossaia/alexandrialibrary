using System;
using System.Collections.Generic;
using System.Drawing;

namespace Alexandria.Media
{
	public interface IImageRendering : IMediaRendering
	{
		string SizeUnit { get; set; }
		Rectangle Size { get; set; }
	}
}
