using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IWriteCriteria<T> :
		ICriteria
		where T : IResource
	{
	}
}
