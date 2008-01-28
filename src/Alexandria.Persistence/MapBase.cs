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
	public abstract class MapBase<Model> : IMap<Model>
	{
		#region Constructors
		public MapBase(IRepository repository, IRecord<Model> record)
		{
			this.repository = repository;
			this.record = record;
			this.type = typeof(Model);
		}
		#endregion
		
		#region Private Fields
		private IRepository repository;
		private IRecord<Model> record;
		private IList<IRelationship> relationships = new List<IRelationship>();
		private Type type;
		#endregion
	
		#region IMap Members
		public IRepository Repository
		{
			get { return repository; }
		}
				
		public IRecord Record
		{
			get { return record; }
		}

		public IList<IRelationship> Relationships
		{
			get { return relationships; }
		}

		public Type Type
		{
			get { return type; }
		}
		
		public virtual Query GetRelationshipQuery(IRelationship relationship)
		{
			return null;
		}
		#endregion
		
		#region IMap<Model> Members
		IRecord<Model> IMap<Model>.Record
		{
			get { return record; }
		}
		
		public abstract Model Lookup(Query query);

		public abstract IList<Model> List(Query query);

		public abstract void Save(Model model);

		public abstract void Delete(Model model);
		#endregion
	}
}
