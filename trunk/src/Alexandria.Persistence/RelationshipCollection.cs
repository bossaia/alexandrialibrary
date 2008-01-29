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
	public class RelationshipCollection : NamedItemCollectionBase<IRelationship>
	{
		#region Constructors
		public RelationshipCollection(ISchema schema)
		{
			this.schema = schema;
		}
		#endregion
		
		#region Private Fields
		private ISchema schema;
		#endregion
		
		#region Public Properties
		public ISchema Schema
		{
			get { return schema; }
		}
		#endregion
		
		#region Public Methods
		public void Add<Parent, Child>(string name, RelationshipType type, IRecord<Parent> parentRecord, IRecord<Child> childRecord, IRecord linkRecord, Field parentField, Field childField, Field linkParentField, Field linkChildField)
		{
			if (!string.IsNullOrEmpty(name))
			{
				if (!base.Contains(name))
				{
					Relationship<Parent, Child> item = new Relationship<Parent, Child>(name, schema, type, parentRecord, childRecord, linkRecord, parentField, childField, linkParentField, linkChildField);
					base.Add(item);
				}
			}
		}
		
		public IRelationship GetRelationshipByParentField(Field parentField)
		{
			foreach (IRelationship relationship in Items)
			{
				if (relationship.ParentField == parentField)
					return relationship;
			}
			
			return null;
		}
		
		public IRelationship<Parent, Child> GetRelationship<Parent, Child>()
		{
			foreach (IRelationship relationship in Items)
			{
				if (relationship.ParentField.Record.DataType == typeof(Parent))
				{
					if (relationship.ChildField.Record.DataType == typeof(Child))
					{
						return relationship as IRelationship<Parent, Child>;
					}
				}
			}
			
			return null;
		}
		#endregion
	}
}
