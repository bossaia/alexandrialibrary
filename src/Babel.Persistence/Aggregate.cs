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
	public abstract class Aggregate : NamedItem
	{
		#region Constructors
		public Aggregate(string name, ISchema schema, Type type)
			: base(name)
		{
			this.schema = schema;
			this.type = type;
			this.root = schema.Entities[type];
		}
		#endregion
		
		#region Private Fields
		private ISchema schema;
		private Type type;
		private Entity root;
		private NamedItemCollection<Map> maps = new NamedItemCollection<Map>();
		#endregion
		
		#region Public Properties
		public ISchema Schema
		{
			get { return schema; }
		}
		
		public Type Type
		{
			get { return type; }
		}
		
		public Entity Root
		{
			get { return root; }
		}
		
		public NamedItemCollection<Map> Maps
		{
			get { return maps; }
		}
		#endregion		
	}
	
	public abstract class Aggregate<T> : Aggregate
	{
		#region Constructors
		public Aggregate(string name, ISchema schema) : base(name, schema, typeof(T))
		{
			this.root = schema.GetEntity<T>();
		}
		#endregion
		
		#region Private
		private Entity<T> root;
		#endregion
		
		#region Public Properties
		public new Entity<T> Root
		{
			get { return root; }
		}
		#endregion
		
		#region Public Methods
		public abstract IList<Tuple> GetTuples(IEnumerable<T> models, DateTime timeStamp);
		
		public abstract IList<T> Load(DataSet dataSet);
		#endregion
	}
}
