using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public static class UriExtensions
	{
		private static readonly string FilePrefix = Uri.UriSchemeFile + ":///";

		public static Uri GetUriFromFileName(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				string escapedFileName = fileName
					//.Replace("%", "%??")
					//.Replace("/", "%??")
					.Replace(" ", "%20");

				return new Uri(FilePrefix + escapedFileName);
			}

			return null;
		}

		public static string GetFileName(this Uri uri)
		{
			if (uri != null)
			{
				string path = uri.ToString();
				if (path.StartsWith(FilePrefix, StringComparison.CurrentCultureIgnoreCase) && path.Length > FilePrefix.Length)
				{
					return path.Substring(FilePrefix.Length, path.Length - FilePrefix.Length);
				}
			}

			return string.Empty;
		}

		public static bool IsValidUri(this string uriString)
		{
			Uri result = null;
			TryParse(uriString, out result);
			return (result != null);
		}

		public static bool TryParse(string uriString, out Uri result)
		{
			try
			{
				result = new Uri(uriString);
				return true;
			}
			catch (Exception)
			{
				result = null;
				return false;
			}
		}
	}
}
