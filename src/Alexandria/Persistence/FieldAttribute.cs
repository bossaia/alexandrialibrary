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

		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignRecordName, string foreignParentFieldName, string foreignChildFieldName, FieldCascades cascade) : this(type, relationship, foreignParentFieldName, cascade)
		{
			this.foreignRecordName = foreignRecordName;
			this.foreignChildFieldName = foreignChildFieldName;
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
		private string foreignRecordName;
		private string foreignParentFieldName;
		private string foreignChildFieldName;
		private Type childType;
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
		#endregion
	}
}
