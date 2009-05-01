using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public struct Link
	{
		private string name;
		private Type type;

		public Link(string name, Type type)
		{
			this.name = name;
			this.type = type;
		}

		public string Name { get { return name; } }
		public Type Type { get { return type; } }

		public override string ToString()
		{
			return name;
		}
	}
}
