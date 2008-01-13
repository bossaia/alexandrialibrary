using System;
using System.Collections.Generic;

namespace Telesophy.Alexandria.Persistence
{
	public interface IConstraint
	{
		string Name { get; }
		ConstraintType Type { get; }
		IList<Field> Fields { get; }
		Predicate<object> Predicate { get; }
	}
}
