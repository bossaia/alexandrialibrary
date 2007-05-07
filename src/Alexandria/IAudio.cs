using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[CLSCompliant(false)]
	public interface IAudio : IPlayable
	{
		float Volume{ get; }
		void SetVolume(float value);
		bool IsMuted { get; }
		void Mute();
		void Unmute();
	}
}
