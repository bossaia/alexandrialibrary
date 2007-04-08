using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IPlugin : IEntity, IProxy
	{
		ICapability Capability { get; }
	}
}
