using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ISubjectLinkCollection<X> : IList<ISubjectLink<X>>
		where X : IEntityType
	{
		ILinkCollection<X, Y> FilterByObjectType<Y>()
			where Y : IEntityType;

		ILinkCollection<X, Y, T> FilterByLinkType<Y, T>()
			where Y : IEntityType
			where T : ILinkType<X, Y>;
	}
}
