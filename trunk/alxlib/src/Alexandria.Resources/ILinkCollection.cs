using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ILinkCollection : IList<ILink>
	{
		ILinkCollection FilterByLinkType<T>()
			where T : ILinkType;

		ISubjectLinkCollection<X> FilterBySubjectType<X>()
			where X : IEntityType;

		IObjectLinkCollection<Y> FilterByObjectType<Y>()
			where Y : IEntityType;
	}

	public interface ILinkCollection<X, Y> : ILinkCollection, IList<ILink<X, Y>>
		where X : IEntityType
		where Y : IEntityType
	{
		new ILinkCollection<X, Y, T> FilterByLinkType<T>()
			where T : ILinkType<X, Y>;
	}

	public interface ILinkCollection<X, Y, T> : ILinkCollection<X, Y>
		where X : IEntityType
		where Y : IEntityType
		where T : ILinkType<X, Y>
	{
		T LinkType { get; }
	}
}
