using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alexandria.Resources;
using Alexandria.Utilities;

namespace Papyrus.Data
{
	public static class DataUtility
	{
		public static string GetString(object value)
		{
			if (value != null)
				return value.ToString();
			else return string.Empty;
		}

		public static Uri GetUri(object value)
		{
			return GetString(value).ToFileUri();
		}

		public static uint GetUInt32(object value)
		{
			uint result = 0;
			uint.TryParse(GetString(value), out result);

			return result;
		}

		public static int GetInt32(object value)
		{
			int result = 0;
			int.TryParse(GetString(value), out result);

			return result;
		}

		public static DateTime GetDateTime(object value)
		{
			DateTime result = DateTime.MinValue;
			DateTime.TryParse(GetString(value), out result);

			return result;
		}

		public static TimeSpan GetTimeSpan(object value)
		{
			TimeSpan result = TimeSpan.Zero;
			TimeSpan.TryParse(GetString(value), out result);

			return result;
		}
	}
}
