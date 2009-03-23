using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class Artist : Resource
	{
		public Artist()
			: base(ResourceTypes.Artist)
		{
		}
	}
}
