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
using System.Collections.ObjectModel;

namespace Telesophy.Alexandria.Persistence
{
	public class Schema : ISchema
	{
		#region Constructors
		public Schema(string name)
		{
			this.name = name;
			this.records = new RecordCollection(this);
			this.relationships = new RelationshipCollection(this);
		}
		#endregion
		
		#region Private Fields
		private string name;
		private RecordCollection records;
		private RelationshipCollection relationships;
		#endregion
		
		#region INamedItem Members
		public string Name
		{
			get { return name; }
		}
		#endregion
		
		#region ISchema Members
		public RecordCollection Records
		{
			get { return records; }
		}
		
		public RelationshipCollection Relationships
		{
			get { return relationships; }
		}
		
		public IRecord<Model> GetRecord<Model>(string name)
		{
			if (Records.Contains(name))
			{
				return (IRecord<Model>)Records[name];
			}
			
			return null;
		}
		
		public RelationshipCollection GetRelationshipsForParentRecord(IRecord parentRecord)
		{
			RelationshipCollection list = new RelationshipCollection(this);
			
			if (parentRecord != null)
			{
				foreach(IRelationship rel in Relationships)
					if (rel.ParentField.Record == parentRecord)
						list.Add(rel);
			}
			
			return list;
		}
		
		public RelationshipCollection GetRelationshipsForChildRecord(IRecord childRecord)
		{
			RelationshipCollection list = new RelationshipCollection(this);

			if (childRecord != null)
			{
				foreach (IRelationship rel in Relationships)
					if (rel.ChildField.Record == childRecord)
						list.Add(rel);
			}
			
			return list;
		}
		#endregion		
	}
}
