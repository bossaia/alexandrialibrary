using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public struct Meta
	{
		public static readonly Meta Empty = default(Meta);

		public Meta(Uri rel, string value)
		{
			this.rel = rel;
			this.value = value;
		}

		private Uri rel;
		private string value;

		public Uri Rel { get { return rel; } }
		public string Value { get { return value; } }
	}
}
