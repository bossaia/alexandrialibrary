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

namespace Telesophy.Alexandria.Persistence
{
	public struct Field : INamedItem
	{
		#region Constructors
		public Field(IRecord record, string name, Type dataType)
		{
			this.record = record;
			this.name = name;
			this.dataType = dataType;
		}
		#endregion
	
		#region Private Fields
		private IRecord record;
		private string name;
		private Type dataType;
		#endregion
		
		#region Public Properties
		public IRecord Record
		{
			get { return record; }
		}
				
		public Type DataType
		{
			get { return dataType; }
		}
		#endregion
		
		#region INamedItem Members
		public string Name
		{
			get { return name; }
		}
		#endregion

		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Field)
			{
				Field other = (Field)obj;
				return (other.ToString() == this.ToString());
			}
			
			return false;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			if (this != Field.Empty)
				return string.Format("{0}.{1}.{2}", record.Schema.Name, record.Name, name);
			else return string.Empty;
		}
		#endregion
		
		#region Static Members
		private static Field empty = new Field();
		
		public static Field Empty
		{
			get { return empty; }
		}
		
		public static bool operator ==(Field f1, Field f2)
		{
			return f1.Equals(f2);
		}
		
		public static bool operator !=(Field f1, Field f2)
		{
			return !f1.Equals(f2);
		}
		#endregion
	}
}
