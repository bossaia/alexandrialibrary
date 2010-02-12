using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Factories
{
	public interface IFactory<T>
		where T : IEntity
	{
		T Create();
	}
}
