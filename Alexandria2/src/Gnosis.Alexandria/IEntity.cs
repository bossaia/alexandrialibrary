using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IEntity
	{
		long Id { get; }
		bool IsChanged();
		bool IsDeleted();
		bool IsNew();
		
		IChangeSet Changes();
		IEnumerable<ILink> Links();
		IEnumerable<ITag> Tags();

		void Delete();

		void AddLink(ILink link);
		void RemoveLink(ILink link);
		
		void AddTag(ITag tag);
		void RemoveTag(ITag tag);
	}
}
