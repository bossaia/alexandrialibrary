using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public struct Link
	{
		public Link(string name, Uri value)
		{
			this.name = name;
			this.value = value;
		}

		private string name;
		private Uri value;

		public string Name
		{
			get { return name; }
		}

		public Uri Value
		{
			get { return value; }
		}
	}
}
