using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ITranslateCriteria<X, Y> :
		IReadCriteria<X>,
		IWriteCriteria<Y>
		where X : IResource
		where Y : IResource
	{
	}
}
