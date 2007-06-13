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

namespace Alexandria.Data
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
		
		public PersistancePropertyAttribute(PersistanceFieldType fieldType, PersistanceLoadType loadType, string foreignKeyName, Type childType) : this(fieldType, loadType)
		{
			this.foreignKeyName = foreignKeyName;
			this.childType = childType;
		}
		#endregion
		
		#region Private Fields
		private PersistanceFieldType fieldType = PersistanceFieldType.None;
		private PersistanceLoadType loadType = PersistanceLoadType.None;
		private string fieldName;
		private string foreignKeyName;
		private Type childType;
		private int ordinal;
		private bool isRequired;
		private bool isUnique;
		private bool isPrimaryKey;
		private object defaultValue;
		private int maxLength;
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
		
		public string ForeignKeyName
		{
			get { return foreignKeyName; }
			set { foreignKeyName = value; }
		}
		
		public Type ChildType
		{
			get { return childType; }
			set { childType = value; }
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
		#endregion
	}
}
