using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface ILinkRepository
	{
		IEnumerable<ILink> GetBySource(IEntity source);
		IEnumerable<ILink> GetByTarget(IEntity target);
	}
}
