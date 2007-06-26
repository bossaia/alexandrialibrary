using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public interface IVideoStream : IMediaStream
	{
		int Width { get; }
		int Height { get; }
	}
}
