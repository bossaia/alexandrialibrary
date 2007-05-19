using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISeekable
	{
		bool IsSeeking { get; }
		SeekDirection SeekDirection { get; }
		int SeekSpeed {get;}
		void Seek(SeekDirection direction);
		void Seek(SeekDirection direction, int speed);
	}
}
