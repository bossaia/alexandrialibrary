using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class ItemNumber : Element<int>
	{
		public ItemNumber(IIdentifier identifier, int value) : base(identifier, value)
		{
		}
	}
}
