using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISeekableForward
	{
		bool IsSeeking { get; }
		SeekDirection SeekDirection { get; }
		int SeekSpeed { get; }
		void SeekForward();
		void SeekForward(int seekSpeed);
		void StopSeeking();
	}
}
