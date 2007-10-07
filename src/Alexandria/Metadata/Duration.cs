using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class Duration : Element<TimeSpan>
	{
		public Duration(IIdentifier identifier, TimeSpan value) : base(identifier, value)
		{
		}
	}
}
