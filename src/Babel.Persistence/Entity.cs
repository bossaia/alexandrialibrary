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
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public abstract class Entity : NamedItem
	{
		#region Constructors
		protected Entity(string name, Schema schema, Type type) : base(name)
		{
			this.schema = schema;
			this.type = type;
		}
		#endregion
		
		#region Private Fields
		private Schema schema;
		private Type type;
		private NamedItemCollection<Field> fields = new NamedItemCollection<Field>();
		private NamedItemCollection<Association> associations = new NamedItemCollection<Association>();
		#endregion
		
		#region Public Properties
		public Schema Schema
		{
			get { return schema; }
		}
		
		public Type Type
		{
			get { return type; }
		}

		public NamedItemCollection<Field> Fields
		{
			get { return fields; }
		}
		
		public NamedItemCollection<Association> Associations
		{
			get { return associations; }
		}		
		#endregion
		
		#region Public Overrides
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Entity)
			{
				Entity other = (Entity)obj;
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
			return string.Format("{0}.{1}", Schema.Namespace, Name);
		}
		#endregion
	}
	
	public abstract class Entity<T> : Entity
	{
		#region Constructors
		protected Entity(string name, Schema schema, Type type) : base(name, schema, type)
		{
		}
		#endregion
	
		#region Public Methods
		public abstract T GetModel(IDictionary<string, object> data);
		
		public abstract IDictionary<string, object> GetTuple(T model);
		#endregion
	}
}
