using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface INamedEntity
		: IEntity
	{
		Name Name { get; }
		ISet<Name> Aliases();

		void Rename(Name name);
		void AddAlias(Name alias);
		void RemoveAlias(Name alias);
	}
}
