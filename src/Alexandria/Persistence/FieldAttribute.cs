#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
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
using System.Text;

namespace Alexandria.Persistence
{
	[AttributeUsage(AttributeTargets.Property)]
	public class FieldAttribute : Attribute
	{
		#region Constructors
		public FieldAttribute(int ordinal) : this(ordinal, FieldConstraints.None)
		{
		}
		
		public FieldAttribute(int ordinal, FieldConstraints constraints) : this(FieldType.Basic, FieldRelationship.None, ordinal, constraints)
		{
		}
		
		public FieldAttribute(FieldType type, FieldRelationship relationship, int ordinal, FieldConstraints constraints)
		{
			this.location = FieldLocation.Local;
			this.type = type;
			this.relationship = relationship;
			this.ordinal = ordinal;
			this.constraints = constraints;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, int ordinal, FieldConstraints constraints, FieldCascades cascades) : this(type, relationship, ordinal, constraints)
		{
			this.cascades = cascades;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, int ordinal, string fieldName, FieldConstraints constraint, FieldCascades cascade) : this(type, relationship, ordinal, constraint, cascade)
		{
			this.fieldName = fieldName;
		}
		
		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignParentFieldName, FieldCascades cascades)
		{
			this.location = FieldLocation.Foreign;
			this.type = type;
			this.relationship = relationship;
			this.foreignParentFieldName = foreignParentFieldName;
			this.cascades = cascades;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignParentFieldName, FieldCascades cascades, FieldLoadOptions loadOptions) : this(type, relationship, foreignParentFieldName, cascades)
		{
			this.loadOptions = loadOptions;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignRecordName, string foreignParentFieldName, string foreignChildFieldName, FieldCascades cascades) : this(type, relationship, foreignParentFieldName, cascades)
		{
			this.foreignRecordName = foreignRecordName;
			this.foreignChildFieldName = foreignChildFieldName;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignRecordName, string foreignParentFieldName, string foreignChildFieldName, FieldCascades cascades, FieldLoadOptions loadOptions) : this(type, relationship, foreignRecordName, foreignParentFieldName, foreignChildFieldName, cascades)
		{
			this.loadOptions = loadOptions;
		}
		#endregion
		
		#region Private Fields
		private FieldLocation location = FieldLocation.None;
		private FieldType type = FieldType.None;
		private FieldRelationship relationship = FieldRelationship.None;
		private int ordinal;
		private string fieldName;
		private FieldConstraints constraints = FieldConstraints.None;
		private FieldCascades cascades = FieldCascades.None;
		private FieldLoadOptions loadOptions = FieldLoadOptions.None;
		private string foreignRecordName;
		private string foreignParentFieldName;
		private string foreignChildFieldName;
		private Type childType;
		private object defaultValue;
		#endregion
		
		#region Public Properties
		public FieldLocation Location
		{
			get { return location; }
		}
		
		public FieldType Type
		{
			get { return type; }
		}
		
		public FieldRelationship Relationship
		{
			get { return relationship; }
		}
		
		public int Ordinal
		{
			get { return ordinal; }
		}

		public string FieldName
		{
			get { return fieldName; }
		}
		
		public FieldConstraints Constraints
		{
			get { return constraints; }
		}
		
		public FieldCascades Cascades
		{
			get { return cascades; }
		}
		
		public FieldLoadOptions LoadOptions
		{
			get { return loadOptions; }
		}
		
		public string ForeignRecordName
		{
			get { return foreignRecordName; }
		}
		
		public string ForeignParentFieldName
		{
			get { return foreignParentFieldName; }
		}
		
		public string ForeignChildFieldName
		{
			get { return foreignChildFieldName; }
		}
		
		public Type ChildType
		{
			get { return childType; }
			set { childType = value; }
		}
		
		public object DefaultValue
		{
			get { return defaultValue; }
			set { defaultValue = value; }
		}
		#endregion
	}
}
