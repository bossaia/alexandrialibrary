using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Media.IO
{
	public interface IAudioStreamFactory
	{
		IAudioStream CreateAudioStream(Uri path);
	}
}
