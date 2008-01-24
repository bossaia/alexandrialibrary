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
	public struct Relationship
	{
		#region Constructors
		public Relationship(string name, ISchema schema, RelationshipType type, Field parentId, Field childId)
		{
			this.name = name;
			this.schema = schema;
			this.type = type;
			this.parentId = parentId;
			this.childId = childId;
		}
		#endregion
	
		#region Private Fields
		private string name;
		private ISchema schema;
		private RelationshipType type;
		private Field parentId;
		private Field childId;
		#endregion
	
		#region Public Properties
		public string Name
		{
			get { return name; }
		}

		public ISchema Schema
		{
			get { return schema; }
		}

		public RelationshipType Type
		{
			get { return type; }
		}
		
		public Field ParentId
		{
			get { return parentId; }
		}
		
		public Field ChildId
		{
			get { return childId; }
		}
		#endregion
		
		#region Static Members
		private static Relationship empty = new Relationship();
		
		public static Relationship Empty
		{
			get { return empty; }
		}
		#endregion
	}
}
