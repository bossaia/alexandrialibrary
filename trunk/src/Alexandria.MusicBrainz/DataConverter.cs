using System;
using System.Collections.Generic;
using System.Text;

namespace AlexandriaOrg.Alexandria.MusicBrainz
{
	public static class DataConverter
	{
		#region Private Static Fields
		private static readonly UTF8Encoding utf8Encoding = new UTF8Encoding();
		#endregion

		#region Public Static Methods

		#region ToDateTime
		public static DateTime ToDateTime(string date)
		{
			if (date != null)
			{
				// because the DateTime parser is *slow*            
				string[] parts = date.Split('-');

				if (parts.Length < 3)
				{
					return DateTime.MinValue;
				}

				try
				{
					// Year, Month, Day
					return new DateTime(Convert.ToInt32(parts[0], System.Globalization.NumberFormatInfo.InvariantInfo), Convert.ToInt32(parts[1], System.Globalization.NumberFormatInfo.InvariantInfo), Convert.ToInt32(parts[2], System.Globalization.NumberFormatInfo.InvariantInfo));
				}
				catch (ArgumentException)
				{
					return DateTime.MinValue;
				}
				catch (InvalidCastException)
				{
					return DateTime.MinValue;
				}
			}
			else throw new ArgumentNullException("date");
		}
		#endregion

		#region ToUtf8
		public static byte[] ToUtf8(string value)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				int length = utf8Encoding.GetByteCount(value);
				byte[] result = new byte[length + 1];
				utf8Encoding.GetBytes(value, 0, value.Length, result, 0);
				result[length] = 0;
				return result;
			}
		}
		#endregion

		#region FromUtf8
		public static string FromUtf8(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				int length = 0;
				while ((length < value.Length) && (value[length] != 0))
				{
					length++;
				}
				return utf8Encoding.GetString(value, 0, length);
			}
		}
		#endregion

		#endregion
	}
}
