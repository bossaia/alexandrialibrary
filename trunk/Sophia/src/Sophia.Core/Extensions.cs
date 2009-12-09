using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sophia.Core
{
	public static class Extensions
	{
		public static Uri ToNamedPipeUri(this string value)
		{
			var uri = Uri.UriSchemeNetPipe + "://" + value;
			return new Uri(uri);
		}

		public static string ToPipeName(this Uri value)
		{
			if (value == null || !value.ToString().StartsWith(Uri.UriSchemeNetPipe) || value.ToString().Length < 5)
				return null;

			var index = Uri.UriSchemeNetPipe.Length + 4;
			var s = value.ToString();
			return s.Substring(index, s.Length - index);
		}
	}
}
