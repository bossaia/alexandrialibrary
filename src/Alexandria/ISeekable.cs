using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISeekable : IMedia
	{
		SeekingState SeekingState { get; }
		int SeekingSpeed {get;}
		void Seek(SeekingState direction);
		void Seek(SeekingState direction, int speed);
	}
}
