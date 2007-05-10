using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlayable : IMedia
	{
		PlaybackState PlaybackState { get; }
		TimeSpan Length { get; }
		TimeSpan Position { get; }
		void Play();
		void Pause();
		void Resume();
		void Seek(TimeSpan position);
		void Stop();
	}
}
