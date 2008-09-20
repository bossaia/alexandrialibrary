using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Title : Element<string>
	{
		public Title(IIdentifier identifier, string value) : base(identifier, value)
		{
		}
	}
}
