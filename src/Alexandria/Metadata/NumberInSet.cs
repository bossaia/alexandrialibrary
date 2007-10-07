using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class NumberInSet : Element<int>
	{
		public NumberInSet(IIdentifier identifier, int value) : base(identifier, value)
		{
		}
	}
}
