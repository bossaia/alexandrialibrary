using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IEntity
	{
		IIdentifier Id { get; }
		ILocation Location { get; }
		string Name { get; }
	}
}
