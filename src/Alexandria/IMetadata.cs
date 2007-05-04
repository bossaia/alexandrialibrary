using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IMetadata
	{
		IIdentifier Id { get; }
		ILocation Location { get; }
		string Name { get; }
	}
}
