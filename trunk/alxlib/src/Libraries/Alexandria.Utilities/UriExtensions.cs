using System;

namespace Alexandria.Utilities
{
	public static class UriExtensions
	{
		public static string ToFileName(this Uri uri)
		{
			if (uri != null)
			{
				string path = uri.ToString();
				if (path.StartsWith(Constants.FilePrefix, StringComparison.CurrentCultureIgnoreCase) && path.Length > Constants.FilePrefix.Length)
				{
					return path.Substring(Constants.FilePrefix.Length, path.Length - Constants.FilePrefix.Length);
				}
			}

			return string.Empty;
		}
	}
}
