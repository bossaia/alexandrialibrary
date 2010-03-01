using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IRepository<T> :
		ISource<IKey<T>, T>,
		ISource<ICriteria<T>, ISet<T>>
		where T : IEntity, IEquatable<T>
	{
	}
}
