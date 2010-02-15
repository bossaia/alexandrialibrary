using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IChangeSet
		: IEnumerable<Change>
	{
		ChangeType ChangeType { get; }
		string Entity { get; }
		long Id { get; }
		IChangeSet Parent { get; }
		IEnumerable<IChangeSet> Children { get; }

		void AddChild(IChangeSet child);
		void SetId(long id);
		void SetParent(IChangeSet parent);
	}
}
