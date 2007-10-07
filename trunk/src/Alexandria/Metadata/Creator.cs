using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Creator : Element<IEntity>
	{
		public Creator(IIdentifier identifier, IEntity value) : base(identifier, value)
		{
		}
	}
}
