#region License (MIT)
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MusicBrainz
{
	public static class Utility
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
