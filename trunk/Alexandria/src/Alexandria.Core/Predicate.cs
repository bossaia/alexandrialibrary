using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alexandria.Core
{
	public class Predicate
	{
		public Predicate(PropertyInfo property, ComparisonOperator op, object value)
			: this(property, op, value, null)
		{
		}

		public Predicate(PropertyInfo property, ComparisonOperator op, object value, IList<string> methods)
		{
			_property = property;
			_op = op;
			_value = value;

			if (methods != null)
				_methods = methods;
		}

		private PropertyInfo _property;
		ComparisonOperator _op;
		object _value;
		private IList<string> _methods = new List<string>();

		private string GetName()
		{
			string name = _property.Name;

			if (_methods.Count > 0)
			{
				//NOTE: We need to go backwards through the list
				for (var i = _methods.Count; i > 0; i--)
				{
					name = string.Format("{0}({1})", _methods[i-1], name);
				}
			}

			return name;
		}

		public PropertyInfo Property
		{
			get { return _property; }
		}

		public string Name
		{
			get { return GetName(); }
		}

		public ComparisonOperator Operator
		{
			get { return _op; }
		}

		public object Value
		{
			get { return _value; }
		}

		public IList<string> Methods
		{
			get { return _methods; }
		}
	}
}
