using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Collections;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria
{
	public abstract class NamedBase
		: EntityBase, INamed
	{
		protected NamedBase(IContext context)
			: base(context)
		{
		}

		protected NamedBase(IContext context, long id)
			: base(context, id)
		{
		}

		private Name _name;

		#region INamed Members

		public Name Name
		{
			get { return _name; }
		}

		public void Rename(Name name)
		{
			_name = name;
		}

		#endregion
	}
}
