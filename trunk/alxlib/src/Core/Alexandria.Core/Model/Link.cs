using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public struct Link
	{
		public static readonly Link Empty = default(Link);

		public Link(Uri rel, Uri value)
		{
			this.rel = rel;
			this.value = value;
		}

		private Uri rel;
		private Uri value;

		public Uri Rel { get { return rel; } }
		public Uri Value { get { return value; } }
	}
}
