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

namespace Telesophy.Babel.Persistence
{
	public abstract class MapBase<Model> : IMap<Model>
	{
		#region Constructors
		protected MapBase(string name, ISchema schema, MapFunction function)
		{
			this.name = name;
			this.schema = schema;
			this.type = typeof(Model);
			this.function = function;
		}
		#endregion
	
		#region Private Fields
		private string name;
		private ISchema schema;
		private Type type;
		private MapFunction function;
		private INamedItemCollection<Field> fields;
		private INamedItemCollection<Association> associations;
		private Field identifierField = Field.Empty;
		#endregion
	
		#region INamedItem Members
		public string Name
		{
			get { return name; }
		}
		#endregion

		#region IMap Members
		public ISchema Schema
		{
			get { return schema; }
		}

		public Type Type
		{
			get { return type; }
		}

		public MapFunction Function
		{
			get { return function; }
		}

		public INamedItemCollection<Field> Fields
		{
			get { return fields; }
		}

		public INamedItemCollection<Association> Associations
		{
			get { return associations; }
		}

		public virtual Field IdentifierField
		{
			get
			{
				if (identifierField == Field.Empty && fields != null)
				{
					foreach (Field field in fields)
					{
						if (field.Function == FieldFunction.Identifier)
						{
							identifierField = field;
							break;
						}
					}
				}
				
				return identifierField;
			}
		}
		#endregion

		#region IMap<Model> Members
		public abstract DataTable GetTable();
		
		public abstract DataTable GetTable(IEnumerable<Model> models);

		public abstract IEnumerable<Model> GetModels(DataTable table);

		public abstract void LoadChildren(IEnumerable<Model> models, IResult result);
		#endregion
	}
}
