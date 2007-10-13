using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class DateOfBirth : Element<DateTime>
	{
		public DateOfBirth(IIdentifier identifier, DateTime value) : base(identifier, value)
		{
		}
	}
}
