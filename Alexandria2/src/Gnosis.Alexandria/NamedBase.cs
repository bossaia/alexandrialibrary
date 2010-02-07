using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public abstract class NamedBase
		: EntityBase, INamed
	{
		protected NamedBase(ILinkRepository linkRepository, ITagRepository tagRepository)
			: base(linkRepository, tagRepository)
		{
		}

		protected NamedBase(ILinkRepository linkRepository, ITagRepository tagRepository, long id)
			: base(linkRepository, tagRepository, id)
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
