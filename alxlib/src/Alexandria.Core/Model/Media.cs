using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class Media : Resource
	{
		public Media()
			: this(ResourceTypes.Media)
		{
		}

		protected Media(Uri type)
			: base(type)
		{
			//NOTE: additional constructor logic goes here
		}
	}
}
