using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IRepository<T> :
		IMessage,
		ISource<IKey<IEntity>, T>,
		ISource<ICriteria<T>, ISet<T>>,
		ISink<IChangeGraph>
		where T : IEntity, IEquatable<T>
	{
	}
}
