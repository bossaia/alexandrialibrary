using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ITagFormat
	{
		string Name { get; }
		IVersion Version { get; }
	}
}
