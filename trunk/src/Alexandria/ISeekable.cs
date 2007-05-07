using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ISeekable : IMedia
	{
		MediaSeekingState SeekingState { get; }
		int SeekingSpeed {get;}
	}
}
