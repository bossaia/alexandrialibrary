using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Catalog : Resource, IAggregate
	{
		public Catalog(Uri id)
			: base(id)
		{
		}

		#region IAggregate Members

		public IEnumerable<T> GetChildren<T>(Link link)
		{
			throw new NotImplementedException();
		}

		public IValidation Validate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
