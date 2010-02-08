using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IEntity
	{
		long Id { get; }
		IEnumerable<Link> Links();
		IEnumerable<Tag> Tags();

		void AddLink(Link link);
		void RemoveLink(Link link);
		void AddTag(Tag tag);
		void RemoveTag(Tag tag);
	}
}
