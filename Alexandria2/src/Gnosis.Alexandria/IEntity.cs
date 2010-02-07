using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IEntity
	{
		long Id { get; }
		ISet<Link> Links();
		ISet<Tag> Tags();

		void AddLink(Link link);
		void RemoveLink(Link link);
		void AddTag(Tag tag);
		void RemoveTag(Tag tag);
	}
}
