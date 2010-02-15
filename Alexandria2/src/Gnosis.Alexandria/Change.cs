using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Change
	{
		public Change(string property, object value)
		{
			_property = property;
			_value = value;
		}

		private readonly string _property;
		private readonly object _value;

		public string Property { get { return _property; } }
		public object Value { get { return _value; } }

		public static readonly Change Empty = new Change(string.Empty, string.Empty);
	}
}
