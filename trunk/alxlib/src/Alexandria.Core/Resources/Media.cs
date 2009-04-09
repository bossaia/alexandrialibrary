using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public class Media : Entity
	{
		public Media(Uri id, IEntityType type)
			: base(id, type)
		{
		}
	}
}
