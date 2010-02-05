using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gnosis.Barbelo
{
	public class TypeRegistry
	{
		private Dictionary<Type, ConstructorInfo> _map = new Dictionary<Type, ConstructorInfo>();
	}
}
