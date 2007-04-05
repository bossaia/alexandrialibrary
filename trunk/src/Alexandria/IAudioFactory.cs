using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioFactory
	{
		IAudio GetAudio(Uri uri);
	}
}
