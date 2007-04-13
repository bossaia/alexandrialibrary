using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlugin : IEntity
	{
		ICapability Capability { get; }
	}
}
