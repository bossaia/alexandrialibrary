using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IText : IMedia
	{		
		IList<byte> TextData { get; }
	}
}
