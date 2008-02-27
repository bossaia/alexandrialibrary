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
using System.Data;
using System.Linq;

namespace Telesophy.Babel.Persistence
{
	public struct Association : INamedItem
	{
		#region Constructors
		public Association(IMap map, string name, Type type, AssociationFunction function)
		{
			this.map = map;
			this.name = name;
			this.type = type;
			this.function = function;
		}
		#endregion
		
		#region Private Fields
		private IMap map;
		private string name;
		private Type type;
		private AssociationFunction function;
		#endregion
		
		#region INamedItem Members
		public string Name
		{
			get { return name; }
		}
		#endregion
		
		#region Public Properties
		public IMap Map
		{
			get { return map; }
		}
		
		public Type Type
		{
			get { return type; }
		}
		
		public AssociationFunction Function
		{
			get { return function; }
		}
		#endregion

		#region Public Methods
		public DataTable GetDataTable()
		{
			return new DataTable();
		}
		#endregion

		#region Public Overrides
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Association)
			{
				Association other = (Association)obj;
				return (this.ToString() == other.ToString());
			}

			return false;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			if (map != null && map.Schema != null && name != null)
				return string.Format("{0}.{1}.{2}", map.Schema.Name, map.Name, name);
			else return string.Empty;
		}
		#endregion

		#region Static Members
		private static Association empty = default(Association);

		public static Association Empty
		{
			get { return empty; }
		}

		public static bool operator ==(Association a1, Association a2)
		{
			return a1.Equals(a2);
		}

		public static bool operator !=(Association a1, Association a2)
		{
			return !a1.Equals(a2);
		}
		#endregion

	}
}
