using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Alexandria.SQLite
{
	internal class TableMap
	{
		#region Constructors
		public TableMap(DataTable table, Type type)
		{
			this.table = table;
			this.type = type;
		}
		#endregion
		
		#region Private Fields
		private DataTable table;
		private Type type;
		private IDictionary<PropertyInfo, TableMap> children = new Dictionary<PropertyInfo, TableMap>();
		private bool idInitialized;
		private Guid id;
		#endregion
		
		#region Public Properties
		public DataTable Table
		{
			get { return table; }
		}
		
		public Type Type
		{
			get { return type; }
		}
		
		public Guid Id
		{
			get
			{
				if (!idInitialized)
				{
					idInitialized = true;
					
					if (table.Rows.Count > 0)
						id = new Guid(table.Rows[0]["Id"].ToString());
				}
			
				return id;
			}
		}
		
		public IDictionary<PropertyInfo, TableMap> Children
		{
			get { return children; }
		}
		#endregion
	}
}
