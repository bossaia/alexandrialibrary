using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Work : Element<string>
	{
		public Work(IIdentifier identifier, string value) : base(identifier, value)
		{
		}
	}
}
