using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IAudioStatus
	{
		MediaBufferState BufferState{get;set;}
		MediaStreamingState StreamingState{get;set;}
		MediaPlaybackState PlaybackState{get;set;}
		MediaSeekingState SeekingState{get;set;}
		float BufferLevel{get;set;}
		bool AllowsLoad{get;}
		bool AllowsStream{get;}
		bool AllowsPlay{get;}
		bool AllowsPause{get;}
		bool AllowsStop{get;}
		bool AllowsSeek{get;}
		void Update(IAudioCommand command);
	}
}
