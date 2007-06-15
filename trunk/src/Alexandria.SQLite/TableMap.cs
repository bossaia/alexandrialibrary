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
		public TableMap(Type type, DataTable table, PersistanceClassAttribute classAttribute, ConstructorInfo constructor)
		{
			this.type = type;
			this.table = table;
			this.classAttribute = classAttribute;
			this.constructor = constructor;
		}
		#endregion
		
		#region Private Fields
		private DataTable table;
		private Type type;
		private PersistanceClassAttribute classAttribute;
		private ConstructorInfo constructor;
		private IDictionary<PropertyMap, TableMap> children = new Dictionary<PropertyMap, TableMap>();
		private bool isFilled;
		#endregion
				
		#region Public Properties
		public Type Type
		{
			get { return type; }
		}
		
		public DataTable Table
		{
			get { return table; }
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
		
		public bool IsFilled
		{
			get { return isFilled; }
		}
		#endregion
		
		#region Public Methods
		public void Lookup(string idField, Guid id)
		{
			isFilled = true;
		}
		#endregion
	}
}
