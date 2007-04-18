using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlayableStatus
	{
		MediaBufferState BufferState { get;set;}
		MediaStreamingState StreamingState { get;set;}
		MediaPlaybackState PlaybackState { get;set;}
		MediaSeekingState SeekingState { get;set;}
		float BufferLevel { get;set;}
	}
}
