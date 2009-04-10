using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Album : Aggregate
	{
		public Album(Uri id)
			: base(id, new EntityType(Schema.Types.Aggregates.Album, "", ""))
		{
		}
	}
}
