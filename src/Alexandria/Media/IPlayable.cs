using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlayable
	{
		PlaybackState PlaybackState { get; }
		void Play();
		void Pause();
		void Resume();		
		void Stop();
	}
}
