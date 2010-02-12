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

		T Get<T>(long id)
			where T : IEntity;

		T Get<T>(string code)
			where T : struct;
	}
}
