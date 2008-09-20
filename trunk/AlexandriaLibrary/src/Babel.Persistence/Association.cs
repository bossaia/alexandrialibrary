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
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public class Association : NamedItem
	{
		#region Constructors
		public Association(string name, Entity parent, Entity child, Relationship relationship, bool isRequired)
			: base(name)
		{
			this.parent = parent;
			this.child = child;
			this.relationship = relationship;
			this.isRequired = isRequired;
		}
		#endregion

		#region Private Constants
		private const string PARENT_IDENTIFIER_NAME = "_ParentId";
		private const string CHILD_IDENTIFIER_NAME = "_ChildId";
		private const string DATE_MODIFIED_FIELD_NAME = "_DateModified";
		private const string SEQUENCE_FIELD_NAME = "_Sequence";
		#endregion
		
		#region Private Fields
		private Entity parent;
		private Entity child;
		private Relationship relationship;
		private bool isRequired;
		#endregion
		
		#region Public Properties
		public Entity Parent
		{
			get { return parent; }
		}
		
		public Entity Child
		{
			get { return child; }
		}
		
		public Relationship Relationship
		{
			get { return relationship; }
		}
				
		public bool IsRequired
		{
			get { return isRequired; }
		}

		public string ParentFieldName
		{
			get { return PARENT_IDENTIFIER_NAME; }
		}

		public string ChildFieldName
		{
			get { return CHILD_IDENTIFIER_NAME; }
		}

		public virtual string DateModifiedFieldName
		{
			get { return DATE_MODIFIED_FIELD_NAME; }
		}
		
		public virtual string SequenceFieldName
		{
			get { return SEQUENCE_FIELD_NAME; }
		}
		#endregion
		
		#region Public Methods
		//public void AddDataRows<ParentIdType, ChildIdType>(DataTable table, ParentIdType parentId, IEnumerable<ChildIdType> childrenIds, DateTime timeStamp)
		//{
		//    if (table != null && parentId != null && childrenIds != null)
		//    {
		//        foreach (ChildIdType childId in childrenIds)
		//        {
		//            DataRow row = table.NewRow();
					
		//            row[ParentFieldName] = parentId;
		//            row[ChildFieldName] = childId;
		//            row[DateModifiedFieldName] = timeStamp;
					
		//            table.Rows.Add(row);
		//        }
		//    }
		//}
		
		public Tuple GetTuple(object parentId, object childId, DateTime timeStamp, int sequence)
		{
			Tuple tuple = new Tuple(Name, this);
			tuple[ParentFieldName] = parentId;
			tuple[ChildFieldName] = childId;
			tuple[DateModifiedFieldName] = timeStamp;
			tuple[SequenceFieldName] = sequence;
			
			return tuple;
		}
		
		public DataTable GetDataTable()
		{
			DataTable table = new DataTable(Name);
			
			table.Columns.Add(ParentFieldName, Parent.Identifier.Type);
			table.Columns.Add(ChildFieldName, Child.Identifier.Type);
			table.Columns.Add(DateModifiedFieldName, typeof(DateTime));
			
			return table;
		}
		#endregion
		
		#region Public Overrides
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Association)
			{
				Association other = (Association)obj;
				return this.ToString().Equals(other.ToString());
			}
			
			return false;
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}", Parent.Schema.Namespace, Parent.Name, Name);
		}
		#endregion
	}
}
