using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistance
{
	[AttributeUsage(AttributeTargets.Constructor)]
	public class ConstructorAttribute : Attribute
	{
		public ConstructorAttribute()
		{
		}
	}
}
