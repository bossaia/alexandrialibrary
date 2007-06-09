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
		private PersistanceLoadType loadType = PersistanceLoadType.None;
		private PersistanceSaveType saveType = PersistanceSaveType.None;
		private Type itemType;
		private bool isPrimaryKey;
		private bool isUnique;
		private bool isChild;
		private bool isCollection;
		private bool isParent;
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
		
		public Type ItemType
		{
			get { return itemType; }
			set { itemType = value; }
		}

		public bool IsPrimaryKey
		{
			get { return isPrimaryKey; }
			set
			{				
				isPrimaryKey = value;
				if (isPrimaryKey) isUnique = true;
			}
		}
		
		public bool IsUnique
		{
			get { return isUnique; }
			set { isUnique = value; }
		}
		
		public bool IsChild
		{
			get { return isChild; }
			set { isChild = value; }
		}
		
		public bool IsParent
		{
			get { return isParent; }
			set { isParent = value; }
		}
		
		public bool IsCollection
		{
			get { return isCollection; }
			set { isCollection = value; }
		}
		#endregion
	}
}
