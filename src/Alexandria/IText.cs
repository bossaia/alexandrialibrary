using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IText : IMedia
	{		
		Encoding Encoding { get; }
		string Text { get; }
	}
}
