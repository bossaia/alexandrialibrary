using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ILocation
	{
		bool IsLocal { get; }
		string AbsolutePath { get; }
		string RelativePath { get; }
	}
}