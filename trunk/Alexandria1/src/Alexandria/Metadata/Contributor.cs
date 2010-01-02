using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Contributor : Element<string>
	{
		public Contributor(IIdentifier identifier, string value) : base(identifier, value)
		{
		}
	}
}
