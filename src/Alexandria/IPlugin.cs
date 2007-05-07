using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlugin
	{
		string Name { get; }
		ILocation Location { get; }
		Version Version { get; }
		//ICapability Capability { get; }
	}
}
