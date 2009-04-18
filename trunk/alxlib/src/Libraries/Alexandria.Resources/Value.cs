using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public abstract class Value : Resource, IValue
	{
		protected Value(Uri id, IValueType type)
			: base(id)
		{
			this.type = type;
		}

		#region Private Members

		private IValueType type;
		private object data;

		#endregion

		#region IValue Members

		public IValueType Type
		{
			get { return type; }
		}

		public object Data
		{
			get { return data; }
			set { data = value; }
		}

		#endregion
	}
}
