using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IObjectAggregate<Y> : IAggregate
		where Y : IEntityType
	{
		new IEntity<Y> Root { get; }
		
		new IObjectLinkCollection<Y> Links { get; }

		void AddLink<T>(IEntity subject)
			where T : IObjectLinkType<Y>;

		void AddLink<T>(IEntity subject, int sequence)
			where T : IObjectLinkType<Y>;

		void AddLinks(IObjectLinkCollection<Y> links);
	}
}
