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
	public abstract class Entity : NamedItem
	{
		#region Constructors
		protected Entity(string name, Schema schema, Type type) : base(name)
		{
			this.schema = schema;
			this.type = type;
		}
		#endregion
		
		#region Private Constants
		private const string PARENT_LINK_FIELD_NAME = "Parent_Id";
		private const string DATE_MODIFIED_FIELD_NAME = "Date_Modified";
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

		public abstract Field Identifier { get; }

		public NamedItemCollection<Field> Fields
		{
			get { return fields; }
		}
		
		public NamedItemCollection<Association> Associations
		{
			get { return associations; }
		}
		
		public virtual string ParentLinkFieldName
		{
			get { return PARENT_LINK_FIELD_NAME; }
		}
		
		public virtual string DateModifiedFieldName
		{
			get { return DATE_MODIFIED_FIELD_NAME; }
		}
		#endregion
		
		#region Public Methods
		public virtual DataTable GetDataTable(string name)
		{
			DataTable table = new DataTable(name);
			
			foreach (Field field in Fields)
			{
				table.Columns.Add(field.Name, field.Type);
			}
			
			table.Columns.Add(DateModifiedFieldName, typeof(DateTime));
			
			return table;
		}
		
		public virtual DataTable GetDataTable(Map map)
		{
			DataTable table = GetDataTable(map.Name);
			table.Columns.Add(ParentLinkFieldName, map.Root.Identifier.Type);
			return table;
		}

		public virtual string GetFieldList()
		{
			StringBuilder list = new StringBuilder();
			
			const string COMMA = ", ";
			const string FORMAT_FIELD = "{0}.{1}";
			
			int i = 0;
			foreach (Field field in Fields)
			{
				i++;
				if (i > 1) list.Append(COMMA);
				list.AppendFormat(FORMAT_FIELD, Name, field.Name);
			}
			
			list.Append(COMMA);
			list.AppendFormat(FORMAT_FIELD, Name, DateModifiedFieldName);
			
			return list.ToString();
		}
		
		public virtual string GetFieldList(Map map)
		{
			string list = GetFieldList();
			if (!string.IsNullOrEmpty(list))
			{
				list += string.Format(", {0}", ParentLinkFieldName);
			}
			
			return list;
		}

		public virtual void AddDataRow(DataTable table, IDataRecord record, IDataConverter dataConverter, Map map)
		{
			if (table != null && record != null)
			{
				DataRow row = table.NewRow();
			
				foreach (Field field in Fields)
				{
					row[field.Name] = dataConverter.GetEntityValue(record[field.Name], field.Type);
				}
				
				if (record.GetOrdinal(DateModifiedFieldName) > -1)
				{
					row[DateModifiedFieldName] = dataConverter.GetEntityValue(record[DateModifiedFieldName], typeof(DateTime));
				}
				
				if (map != null && record.GetOrdinal(ParentLinkFieldName) > -1)
				{
					row[ParentLinkFieldName] = dataConverter.GetEntityValue(record[ParentLinkFieldName], map.Root.Identifier.Type);
				}
			}
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
