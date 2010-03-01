using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IChange
		: IEntity
	{
		Type Type { get; }
		string Property { get; }
		object Value { get; }
	}
}
