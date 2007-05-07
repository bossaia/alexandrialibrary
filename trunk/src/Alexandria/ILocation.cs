using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface ILocation
	{
		string Path { get; }
		bool IsLocal { get; }
		bool RequiresAuthentication { get; }
	}
}