using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Persistence
{
	public class Filter : IFilter
	{
		public Filter(FilterType type, string name, Operator op, object value)
			: this(type, name, op, value, null)
		{
		}

		public Filter(FilterType type, string name, Operator op, object value, IFilter child)
		{
			this.type = type;
			this.name = name;
			this.op = op;
			this.value = value;
			this.child = child;
		}

		#region Private Members

		private FilterType type;
		private string name;
		private Operator op;
		private object value;
		private IFilter child;

		#endregion

		#region IFilter Members

		public FilterType Type
		{
			get { return type; }
		}

		public string Name
		{
			get { return name; }
		}

		public Operator Operator
		{
			get { return op; }
		}

		public object Value
		{
			get { return value; }
		}

		public IFilter Child
		{
			get { return child; }
		}

		#endregion
	}
}
