using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public abstract class RepositoryBase
	{
		protected RepositoryBase(IContext context)
		{
			_context = context;
		}

		private readonly IContext _context;

		protected IContext Context
		{
			get { return _context; }
		}
	}
}
