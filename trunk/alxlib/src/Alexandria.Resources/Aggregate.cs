using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public abstract class Aggregate : Entity
	{
		protected Aggregate(Uri id, IEntityType type)
			: base(id, type)
		{
		}
	}
}
