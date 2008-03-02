#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
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
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence.SQLite
{
	public class DataConverter : IDataConverter
	{
		#region IDataConverter Members
		public object GetValue(object value, Type outputType)
		{
			Type inputType = value.GetType();
			
			switch (inputType.Name)
			{
				case "DateTime":
					if (outputType == typeof(long))
						return ((DateTime)value).ToFileTimeUtc();
					break;
				case "Int64":
					if (outputType == typeof(DateTime))
						return DateTime.FromFileTimeUtc((long)value);
					else if (outputType == typeof(TimeSpan))
						return TimeSpan.FromTicks((long)value);
					break;
				case "String":
					if (outputType == typeof(Uri))
						return new Uri((string)value);
					break;
				case "TimeSpan":
					if (outputType == typeof(long))
						return ((TimeSpan)value).Ticks;
					break;
				case "Uri":
					if (outputType == typeof(string))
						return ((Uri)value).ToString();
					break;
				default:
					break;
			}
			
			return value;
		}
		
		public Type GetEngineType(Type type)
		{
			switch (type.Name)
			{
				case "DateTime":
					return typeof(long);
				case "TimeSpan":
					return typeof(long);
				case "Uri":
					return typeof(string);
				default:
					return type;
			}
		}
		#endregion
	}
}
