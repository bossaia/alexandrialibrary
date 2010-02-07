using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct TagType
	{
		public TagType(string name)
		{
			_name = name;
		}

		private string _name;

		public string Name
		{
			get { return _name; }
		}
	}
}
