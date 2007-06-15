using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Alexandria.Data;

namespace Alexandria.SQLite
{
	internal class TableMap
	{
		#region Constructors
		public TableMap(DataTable table, Type type, PersistanceClassAttribute classAttribute, ConstructorInfo constructor)
		{
			this.table = table;
			this.type = type;			
			this.classAttribute = classAttribute;
			this.constructor = constructor;
		}
		
		public TableMap(DataTable table, Type type, Guid id, PersistanceClassAttribute classAttribute) : this(table, type, id, classAttribute, null)
		{
		}

		public TableMap(DataTable table, Type type, Guid id, PersistanceClassAttribute classAttribute, ConstructorInfo constructor) : this(table, type, classAttribute, constructor)
		{
			this.id = id;
			idInitialized = true;
		}
		#endregion
		
		#region Private Fields
		private DataTable table;
		private Type type;
		private PersistanceClassAttribute classAttribute;
		private ConstructorInfo constructor;
		private IDictionary<PropertyMap, TableMap> children = new Dictionary<PropertyMap, TableMap>();
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
		
		public PersistanceClassAttribute ClassAttribute
		{
			get { return classAttribute; }
		}
		
		public ConstructorInfo Constructor
		{
			get { return constructor; }
		}
				
		public IDictionary<PropertyMap, TableMap> Children
		{
			get { return children; }
		}
		#endregion
	}
}
