using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public interface IAudioStream : IMediaStream
	{
		float Volume { get; set; }
		bool IsMuted { get; set; }
	}
}
