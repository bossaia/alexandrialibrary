using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class TotalInSet : Element<int>
	{
		public TotalInSet(IIdentifier identifier, int value) : base(identifier, value)
		{
		}
	}
}
