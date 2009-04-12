using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class ObjectAggregate : Entity
	{
		public ObjectAggregate(Uri id, IEntityType type)
			: base(id, type)
		{
		}
	}
}
