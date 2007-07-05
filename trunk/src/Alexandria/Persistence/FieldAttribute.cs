using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[AttributeUsage(AttributeTargets.Property)]
	public class FieldAttribute : Attribute
	{
		#region Constructors
		public FieldAttribute(int ordinal) : this(ordinal, FieldConstraint.None)
		{
		}
		
		public FieldAttribute(int ordinal, FieldConstraint constraint) : this(FieldType.Basic, FieldRelationship.None, ordinal, constraint)
		{
		}
		
		public FieldAttribute(FieldType type, FieldRelationship relationship, int ordinal, FieldConstraint constraint)
		{
			this.location = FieldLocation.Local;
			this.type = type;
			this.relationship = relationship;
			this.ordinal = ordinal;
			this.constraint = constraint;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, int ordinal, FieldConstraint constraint, FieldCascade cascade) : this(type, relationship, ordinal, constraint)
		{
			this.cascade = cascade;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, int ordinal, string fieldName, FieldConstraint constraint, FieldCascade cascade) : this(type, relationship, ordinal, constraint, cascade)
		{
			this.fieldName = fieldName;
		}
		
		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignParentFieldName, FieldCascade cascade)
		{
			this.location = FieldLocation.Foreign;
			this.type = type;
			this.relationship = relationship;
			this.foreignParentFieldName = foreignParentFieldName;
			this.cascade = cascade;
		}

		public FieldAttribute(FieldType type, FieldRelationship relationship, string foreignRecordName, string foreignParentFieldName, string foreignChildFieldName, FieldCascade cascade) : this(type, relationship, foreignParentFieldName, cascade)
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
		private FieldConstraint constraint = FieldConstraint.None;
		private FieldCascade cascade = FieldCascade.None;
		private string foreignRecordName;
		private string foreignParentFieldName;
		private string foreignChildFieldName;
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
		
		public FieldConstraint Constraint
		{
			get { return constraint; }
		}
		
		public FieldCascade Cascade
		{
			get { return cascade; }
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
		#endregion
	}
}
