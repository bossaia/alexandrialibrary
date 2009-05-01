using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Track : Resource, IAggregate
	{
		public Track(Uri id)
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
