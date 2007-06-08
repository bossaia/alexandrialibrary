using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[AttributeUsage(AttributeTargets.Constructor|AttributeTargets.Property)]
	public class PersistanceOptionsAttribute : Attribute
	{
		#region Constructors
		public PersistanceOptionsAttribute()
		{
		}
		
		public PersistanceOptionsAttribute(PersistanceLoadType loadType, PersistanceSaveType saveType)
		{
			this.loadType = loadType;
			this.saveType = saveType;
		}
		
		public PersistanceOptionsAttribute(string tableName, PersistanceLoadType defaultLoadType, PersistanceSaveType defaultSaveType) : this(defaultLoadType, defaultSaveType)
		{
			this.tableName = tableName;
		}
		#endregion
		
		#region Private Fields
		private string tableName = string.Empty;
		private string fieldName = string.Empty;
		private int ordinal;
		private bool isPrimaryKey;
		private PersistanceLoadType loadType = PersistanceLoadType.None;
		private PersistanceSaveType saveType = PersistanceSaveType.None;
		#endregion
		
		#region Public Properties
		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}
		
		public string FieldName
		{
			get { return fieldName; }
			set { fieldName = value; }
		}
		
		public int Ordinal
		{
			get { return ordinal; }
			set { ordinal = value; }
		}
		
		public bool IsPrimaryKey
		{
			get { return isPrimaryKey; }
			set { isPrimaryKey = value; }
		}
		
		public PersistanceLoadType LoadType
		{
			get { return loadType; }
			set { loadType = value; }
		}
		
		public PersistanceSaveType SaveType
		{
			get { return saveType; }
			set { saveType = value; }
		}
		#endregion
	}
}
