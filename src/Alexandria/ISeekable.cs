using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISeekable : IMedia
	{
		SeekingState SeekingState { get; }
		int SeekingSpeed {get;}
	}
}
