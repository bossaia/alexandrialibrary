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
	public class Relationship<Parent, Child> : IRelationship<Parent, Child>
	{
		#region Constructors
		public Relationship(string name, ISchema schema, RelationshipType type, IRecord<Parent> parentRecord, IRecord<Child> childRecord, Field parentField, Field childField) : this(name, schema, type, parentRecord, childRecord, null, parentField, childField, Field.Empty, Field.Empty)
		{
		
		}
		
		public Relationship(string name, ISchema schema, RelationshipType type, IRecord<Parent> parentRecord, IRecord<Child> childRecord, IRecord linkRecord, Field parentField, Field childField, Field linkParentField, Field linkChildField)
		{
			this.name = name;
			this.schema = schema;
			this.type = type;
			this.parentRecord = parentRecord;
			this.childRecord = childRecord;
			this.linkRecord = linkRecord;
			this.parentField = parentField;
			this.childField = childField;
			this.linkParentField = linkParentField;
			this.linkChildField = linkChildField;
		}
		#endregion
	
		#region Private Fields
		private ISchema schema;
		private string name;
		private RelationshipType type;
		private IRecord<Parent> parentRecord;
		private IRecord<Child> childRecord;
		private IRecord linkRecord;
		private Field parentField;
		private Field childField;
		private Field linkParentField;
		private Field linkChildField;
		#endregion
	
		#region INamedItem Members
		public string Name
		{
			get { return name; }
		}
		#endregion
	
		#region IRelationship Members
		public ISchema Schema
		{
			get { return schema; }
		}

		public RelationshipType Type
		{
			get { return type; }
		}

		public IRecord LinkRecord
		{
			get { return linkRecord; }
		}
		
		public Field ParentField
		{
			get { return parentField; }
		}
		
		public Field ChildField
		{
			get { return childField; }
		}
		
		public Field LinkParentField
		{
			get { return linkParentField; }
		}
		
		public Field LinkChildField
		{
			get { return linkChildField; }
		}
		
		public Query GetListChildrenQuery(Query parentQuery)
		{
			if (parentQuery != null && parentQuery.Filters.Count > 0)
			{
				Query listChildrenQuery = new Query("List children for " + Name);
				listChildrenQuery.Filters.Add(parentQuery.Filters[0]);
			
				if (LinkRecord != null)
				{
					listChildrenQuery.Filters.Add(new Filter(LinkParentField, Operator.OnInnerJoin, ParentField));
					listChildrenQuery.Filters.Add(new Filter(ChildField, Operator.OnInnerJoin, LinkChildField));
				}
				else
				{
					listChildrenQuery.Filters.Add(new Filter(ChildField, Operator.OnInnerJoin, ParentField));
				}
				
				return listChildrenQuery;
			}
			
			return null;
		}
		#endregion
		
		#region IRelationship<Parent, Child> Members
		public IRecord<Parent> ParentRecord
		{
			get { return parentRecord; }
		}
		
		public IRecord<Child> ChildRecord
		{
			get { return childRecord; }
		}
		#endregion
	}
}
