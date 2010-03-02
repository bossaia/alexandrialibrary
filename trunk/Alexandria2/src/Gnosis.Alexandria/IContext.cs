using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IContext
	{
		T Get<T>()
			where T : IEntity;
		
		T Get<T>(IKey<IEntity> id)
			where T : IEntity;
		
		ISet<T> Get<T>(ICriteria<T> criteria)
			where T : IEntity, IEquatable<T>;
	}
}
