/***************************************************************************
 *  Copyright (C) 2006 Dan Poage
 * 
 *  Based on Utilities.cs by Aaron Bockover (aaron@aaronbock.net)
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Alexandria
{
	public static class DataConverter
	{
		#region GetHashCode
		public static int GetHashCode(string value)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			MD5 md5 = MD5.Create();
			byte[] hashBytes = md5.ComputeHash(encoding.GetBytes(value));
			return Convert.ToInt32(hashBytes, System.Globalization.NumberFormatInfo.InvariantInfo);
		}
		
		public static int GetHashCode(byte[] value)
		{
			MD5 md5 = MD5.Create();
			byte[] hashBytes = md5.ComputeHash(value);
			return Convert.ToInt32(hashBytes, System.Globalization.NumberFormatInfo.InvariantInfo);
		}
		#endregion				
	}
}
