using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IObjectLinkCollection<Y> : IList<IObjectLink<Y>>
		where Y : IEntityType
	{
		ILinkCollection<X, Y> FilterBySubjectType<X>()
			where X : IEntityType;

		ILinkCollection<X, Y, T> FilterByLinkType<X, T>()
			where X : IEntityType
			where T : ILinkType<X, Y>;
	}
}
