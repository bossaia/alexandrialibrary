using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Alexandria
{
	public interface IVisible
	{
		//Rectangle Dimensions { get; }
		float Hue { get; }
		float Saturation { get; }
		float Brightness { get; }
		float Contrast { get; }
	}
}
