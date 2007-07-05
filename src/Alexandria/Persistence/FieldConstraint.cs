using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[Flags]
	public enum FieldConstraint
	{
		None = 0,
		Required = 1,
		Unique  = 2,
		Key = Required | Unique
	}
}
