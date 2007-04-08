using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlayable : IResource
	{
		TimeSpan Length { get; }
		TimeSpan Position { get; }
		//bool IsPlaying { get; }
		//bool IsPaused { get; }
		void Play();
		void Pause();
		void Seek(TimeSpan position);
		void Stop();
	}
}
