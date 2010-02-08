using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface ILinkRepository
	{
		IEnumerable<Link> GetBySource(IEntity source);
		IEnumerable<Link> GetByTarget(IEntity target);
	}
}
