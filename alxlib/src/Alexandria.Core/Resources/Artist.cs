using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Resources
{
	public class Artist : Entity
	{
		public Artist(Uri id, IEntityType type)
			: base(id, type)
		{
		}
	}
}
