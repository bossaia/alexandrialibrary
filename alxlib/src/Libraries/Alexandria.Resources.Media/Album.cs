using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class Album : Medium
	{
		public Album(Uri id)
			: base(id)
		{
		}

		#region IAggregate Members

		public override IValidation Validate()
		{
			return base.Validate();
		}

		#endregion
	}
}
