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
	public abstract class MapBase : IMap
	{
		#region Constructors
		public MapBase(Schema schema, string recordName, Type type)
		{
			if (schema != null && type != null)
			{
				record = schema.AddRecord(recordName);
				this.type = type;
			}
			else throw new ArgumentNullException("Could not instantiate this map - the schema and/or type were undefined");
		}
		#endregion
		
		#region Private Fields
		private Record record;
		private IList<Relationship> relationships = new List<Relationship>();
		private Type type;
		#endregion
	
		#region IMap Members
		public Record Record
		{
			get { return record; }
		}

		public IList<Relationship> Relationships
		{
			get { return relationships; }
		}

		public Type Type
		{
			get { return type; }
		}

		public abstract Model Lookup<Model>(Query query);

		public abstract IList<Model> List<Model>(Query query);

		public abstract void Save<Model>(Model model);

		public abstract void Delete<Model>(Model model);
		#endregion
	}
}
