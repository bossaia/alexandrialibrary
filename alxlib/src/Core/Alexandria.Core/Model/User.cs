using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class User : Resource
	{
		public User()
			: base(ResourceTypes.User)
		{
		}
	}
}
