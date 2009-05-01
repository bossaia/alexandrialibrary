using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Medium : Resource, IAggregate
	{
		public Medium(Uri id)
			: base(id)
		{
		}

		#region IAggregate Members

		public virtual IEnumerable<T> GetChildren<T>(Link link)
		{
			throw new NotImplementedException();
		}

		public virtual IValidation Validate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
