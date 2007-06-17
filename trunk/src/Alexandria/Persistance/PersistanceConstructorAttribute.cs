using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistance
{
	[AttributeUsage(AttributeTargets.Constructor)]
	public class PersistanceConstructorAttribute : Attribute
	{
		public PersistanceConstructorAttribute()
		{
		}
	}
}
