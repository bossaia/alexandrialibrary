using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Barbelo
{
	public class ObjectFactory
	{
		public void Configure()
		{
		}

		public T GetInstance<T>()
		{
			return default(T);
		}
	}
}
