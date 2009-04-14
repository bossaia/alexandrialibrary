using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IObjectLink<Y> : ILink
		where Y : IEntityType
	{
		new IObjectLinkType<Y> Type { get; }
		new IEntity<Y> Object { get; }
	}
}
