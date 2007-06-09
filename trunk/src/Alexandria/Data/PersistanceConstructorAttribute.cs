using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Data
{
	[AttributeUsage(AttributeTargets.Constructor)]
	public class PersistanceConstructorAttribute : Attribute
	{
		public PersistanceConstructorAttribute()
		{
		}
	}
}
