using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baoth
{
	public struct INameEncoding
	{
		public INameEncoding(string value)
		{
			this.value = value;
		}

		private string value;

		public string Value
		{
			get { return value; }
		}
	}
}
