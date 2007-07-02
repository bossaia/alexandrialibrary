#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PropertyAttribute : Attribute
	{
		#region Constructors
		public PropertyAttribute()
		{
		}
		
		public PropertyAttribute(int ordinal)
		{
			this.ordinal = ordinal;
		}
		
		public PropertyAttribute(int ordinal, FieldType fieldType) : this(ordinal)
		{
			this.fieldType = fieldType;
		}
		
		public PropertyAttribute(int ordinal, FieldType fieldType, LoadType loadType, StoreType storeType) : this(ordinal, fieldType)
		{			
			this.loadType = loadType;
			this.storeType = storeType;
		}
		
		public PropertyAttribute(FieldType fieldType, LoadType loadType, StoreType storeType, string foreignKeyName) : this(-1, fieldType, loadType, storeType)
		{
			this.foreignKeyName = foreignKeyName;
		}

		public PropertyAttribute(int ordinal, FieldType fieldType, LoadType loadType, StoreType storeType, string foreignKeyName) : this(fieldType, loadType, storeType, foreignKeyName)
		{
			this.ordinal = ordinal;
		}
		#endregion
		
		#region Private Fields
		private int ordinal;
		private FieldType fieldType = FieldType.Basic;
		private LoadType loadType = LoadType.Constructor;
		private StoreType storeType = StoreType.Basic;
		private string fieldName;
		private string foreignKeyName;
		private bool isRequired;
		private bool isUnique;
		private bool isPrimaryKey;
		private object defaultValue;
		private int maxLength;
		private bool cascadeSave;
		private bool cascadeDelete;
		#endregion
		
		#region Public Properties
		public int Ordinal
		{
			get { return ordinal; }
			set { ordinal = value; }
		}

		public FieldType FieldType
		{
			get { return fieldType; }
			set { fieldType = value; }
		}
		
		public LoadType LoadType
		{
			get { return loadType; }
			set { loadType = value; }
		}

		public StoreType StoreType
		{
			get { return storeType; }
			set { storeType = value; }
		}
		
		public string FieldName
		{
			get { return fieldName; }
			set { fieldName = value; }
		}
				
		public string ForeignKeyName
		{
			get { return foreignKeyName; }
			set { foreignKeyName = value; }
		}
				
		public bool IsRequired
		{
			get { return isRequired; }
			set { isRequired = value; }
		}
		
		public bool IsUnique
		{
			get { return isUnique; }
			set { isUnique = value;	}
		}
		
		public bool IsPrimaryKey
		{
			get { return isPrimaryKey; }
			set { isPrimaryKey = value; }
		}
		
		public object DefaultValue
		{
			get { return defaultValue; }
			set { defaultValue = value; }
		}
		
		public int MaxLength
		{
			get { return maxLength; }
			set { maxLength = value; }
		}
		
		public bool CascadeSave
		{
			get { return cascadeSave; } 
			set { cascadeSave = value; }
		}
		
		public bool CascadeDelete
		{
			get { return cascadeDelete; }
			set { cascadeDelete = value; }
		}
		#endregion
	}
}
