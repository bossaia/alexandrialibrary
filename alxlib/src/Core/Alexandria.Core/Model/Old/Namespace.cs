using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core.Model
{
	public struct Namespace
	{
		public static readonly Namespace Empty = default(Namespace);

		public Namespace(Uri value) : this(string.Empty, value)
		{
		}

		public Namespace(string prefix, Uri value)
		{
			this.prefix = prefix ?? string.Empty;
			this.value = value;
		}

		private string prefix;
		private Uri value;

		public string Prefix { get { return prefix; } }
		public Uri Value { get { return value; } }
	}
}
