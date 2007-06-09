using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PersistancePropertyAttribute : Attribute
	{
		#region Constructors
		public PersistancePropertyAttribute()
		{
		}
		
		public PersistancePropertyAttribute(PersistanceFieldType fieldType)
		{
			this.fieldType = fieldType;
		}
		
		public PersistancePropertyAttribute(PersistanceFieldType fieldType, PersistanceLoadType loadType) : this(fieldType)
		{			
			this.loadType = loadType;
		}
		#endregion
		
		#region Private Fields
		private PersistanceFieldType fieldType = PersistanceFieldType.None;
		private PersistanceLoadType loadType = PersistanceLoadType.None;
		private string fieldName;
		private int ordinal;
		private bool isRequired;
		private bool isUnique;
		private bool isPrimaryKey;
		#endregion
		
		#region Public Properties
		public PersistanceFieldType FieldType
		{
			get { return fieldType; }
			set { fieldType = value; }
		}
		
		public PersistanceLoadType LoadType
		{
			get { return loadType; }
			set { loadType = value; }
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
		
		public bool IsRequired
		{
			get { return isRequired; }
			set { isRequired = value; }
		}
		
		public bool IsUnique
		{
			get { return isUnique; }
			set
			{
				isUnique = value;
				if (!isUnique && isPrimaryKey) isPrimaryKey = false;
			}
		}
		
		public bool IsPrimaryKey
		{
			get { return isPrimaryKey; }
			set
			{
				isPrimaryKey = value;
				if (isPrimaryKey)
				{
					isRequired = true;
					isUnique = true;
				}
			}
		}
		#endregion
	}
}
