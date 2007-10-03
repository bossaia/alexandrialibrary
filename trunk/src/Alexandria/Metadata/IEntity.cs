using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface IEntity
	{
		Uri Identifier { get; }
		string Name { get; }
	}
}
