using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlayable : IMedia
	{
		TimeSpan Length { get; }
		TimeSpan Position { get; }
		//bool IsPlaying { get; }
		//bool IsPaused { get; }
		void Play();
		void Pause();
		void Resume();
		void Seek(TimeSpan position);
		void Stop();
	}
}
