using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public class ReleaseDate : Element<DateTime>
	{
		public ReleaseDate(IIdentifier identifier, DateTime value) : base(identifier, value)
		{
		}
	}
}
