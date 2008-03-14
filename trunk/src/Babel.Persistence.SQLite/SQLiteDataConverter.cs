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
	public class SQLiteDataConverter : IDataConverter
	{
		#region Constructors
		public SQLiteDataConverter()
		{
		}
		#endregion
	
		#region IDataConverter Members
		public object GetEntityValue(object engineValue, Type entityType)
		{
			throw new NotImplementedException();
		}

		public object GetEngineValue(object entityValue)
		{
			if (entityValue != null)
			{
				switch (entityValue.GetType().Name)
				{
					case "String":
					case "Guid":
					case "Uri":
						return entityValue.ToString();
					case "DateTime":
						return ((DateTime)entityValue).ToString("s");
					case "TimeSpan":
						TimeSpan span = (TimeSpan)entityValue;
						return string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
					default:
						return entityValue;
				}
			}
			else return null;
		}
		#endregion
	}
}
