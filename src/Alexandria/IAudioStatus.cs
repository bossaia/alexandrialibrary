using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioStatus
	{
		AudioBufferState BufferState{get;}
		AudioPlaybackState PlaybackState{get;}
		bool IsSeeking{get;}
		float BufferLevel{get;}
		bool AllowsLoad{get;}
		bool AllowsStream{get;}
		bool AllowsPlay{get;}
		bool AllowsPause{get;}
		bool AllowsStop{get;}
		bool AllowsSeek{get;}
		void Update(IAudioCommand command);
	}
}
