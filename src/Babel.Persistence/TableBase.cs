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

namespace Telesophy.Babel.Persistence
{
	public abstract class TableBase : ITable
	{
		#region Constructors
		protected TableBase(string name, IDatabase database, TableType type, bool isTemporary)
		{
			this.name = name;
			this.database = database;
			this.type = type;
			this.isTemporary = isTemporary;
			this.columns = new NamedItemCollection<IColumn>();
			this.constraints = new NamedItemCollection<IConstraint>();
		}
		#endregion
		
		#region Private Fields
		private string name;
		private IDatabase database;
		private TableType type;
		private bool isTemporary;
		private INamedItemCollection<IColumn> columns;
		private INamedItemCollection<IConstraint> constraints;
		#endregion
		
		#region INamedItem Members
		public string Name
		{
			get { return name; }
		}
		#endregion

		#region ITable Members
		public IDatabase Database
		{
			get { return database; }
		}

		public TableType Type
		{
			get { return type; }
		}

		public bool IsTemporary
		{
			get { return isTemporary; }
		}

		public INamedItemCollection<IColumn> Columns
		{
			get { return columns; }
		}

		public INamedItemCollection<IConstraint> Constraints
		{
			get { return constraints; }
		}
		#endregion
	}
}
