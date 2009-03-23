using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public class Link : Resource
	{
		public Link()
			: base(ResourceTypes.Link)
		{
		}

		/// <summary>
		/// The value of this link
		/// </summary>
		public Uri Value { get; set; }
	}
}
