using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface IAggregate
	{
		IEntity Root { get; }
		
		ILinkCollection Links { get; }

		void AddLink<T>(IEntity subject, IEntity obj)
			where T : ILinkType;

		void AddLink<T>(IEntity subject, IEntity obj, int sequence)
			where T : ILinkType;
		
		void AddLinks(ILinkCollection links);

		void RemoveLink(ILink link);
	}
}
