using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IProfile
	{
		string Name { get; }
		IList<string> Paths { get; }
	}
}
