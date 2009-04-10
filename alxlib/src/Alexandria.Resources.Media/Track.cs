using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Track : Aggregate
	{
		public Track(Uri id, IEntityType type)
			: base(id, type)
		{
		}
	}
}
