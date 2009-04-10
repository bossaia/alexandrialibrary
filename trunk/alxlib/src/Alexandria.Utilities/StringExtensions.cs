using System;

namespace Alexandria.Utilities
{
	public static class StringExtensions
	{
		#region Private Static Members

		private static bool TryParse(string uriString, out Uri result)
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

		#endregion

		public static Uri ToFileUri(this string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				string escapedFileName = fileName
					//.Replace("%", "%??")
					//.Replace("/", "%??")
					.Replace(" ", "%20");

				return new Uri(Constants.FilePrefix + escapedFileName);
			}

			return null;
		}

		public static bool IsValidUri(this string uriString)
		{
			Uri result = null;
			TryParse(uriString, out result);
			return (result != null);
		}
	}
}
