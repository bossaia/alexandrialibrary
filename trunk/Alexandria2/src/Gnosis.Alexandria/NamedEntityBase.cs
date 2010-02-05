using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public abstract class NamedEntityBase
		: EntityBase, INamedEntity
	{
		private Name _name;
		private Set<Name> _aliases = new Set<Name>();

		#region INamedEntity Members

		public Name Name
		{
			get { return _name; }
		}

		public ISet<Name> Aliases()
		{
			return _aliases;
		}

		public void Rename(Name name)
		{
			_name = name;
		}

		public void AddAlias(Name alias)
		{
			_aliases.Add(alias);
		}

		public void RemoveAlias(Name alias)
		{
			_aliases.Remove(alias);
		}

		#endregion
	}
}
