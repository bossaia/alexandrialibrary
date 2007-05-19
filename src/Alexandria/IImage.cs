using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IImage : IMedia, IVisible
	{		
		//IList<byte> ImageData { get; }
		System.Drawing.Image Image { get; }
	}
}
