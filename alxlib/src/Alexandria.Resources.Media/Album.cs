using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Album : Entity
	{
		public Album(Uri id)
			: base(id, Schema.Types.Entities.AlbumType)
		{
		}
	}
}
