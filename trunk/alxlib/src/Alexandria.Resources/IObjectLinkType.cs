using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IObjectLinkType<Y> : ILinkType
		where Y: IEntityType
	{
		IObjectLink<Y> CreateLink(IEntity subject, IEntity<Y> obj);
		IObjectLink<Y> CreateLink(IEntity subject, IEntity<Y> obj, int sequence);
	}
}
