using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISeekableBackward
	{
		bool IsSeeking { get; }
		SeekDirection SeekDirection { get; }
		int SeekSpeed { get; }
		void SeekBackward();
		void SeekBackward(int seekSpeed);
		void StopSeeking();
	}
}
