using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class Tag : Resource
	{
		public Tag()
			: base(ResourceTypes.Tag)
		{
		}

		/// <summary>
		/// The value of this tag
		/// </summary>
		public string Value { get; set; }
	}
}
