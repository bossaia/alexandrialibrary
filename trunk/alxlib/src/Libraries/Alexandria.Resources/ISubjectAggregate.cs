using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ISubjectAggregate<X> : IAggregate
		where X: IEntityType
	{
		new IEntity<X> Root { get; }
		
		new ISubjectLinkCollection<X> Links { get; }
		
		void AddLink<T>(IEntity obj)
			where T : ISubjectLinkType<X>;

		void AddLink<T>(IEntity obj, int sequence)
			where T : ISubjectLinkType<X>;

		void AddLinks(ISubjectLinkCollection<X> links);
	}
}
