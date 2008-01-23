#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2008 Dan Poage
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
using System.Collections.ObjectModel;

namespace Telesophy.Alexandria.Persistence
{
	public abstract class Record
	{
		#region Constructors
		public Record(string name, Schema schema) : this(name, schema, null, null)
		{
		}
		
		public Record(string name, Schema schema, List<Field> fields, List<Constraint> constraints)
		{
			this.name = name;
			this.schema = schema;
			
			if (fields != null)
			{
				this.fields = fields;
				SynchronizeFields();
			}
			
			if (constraints != null)
			{
				this.constraints = constraints;
				SynchronizeConstraints();
			}

			Schema.AddRecord(this);
		}
		#endregion
	
		#region Private Fields
		private string name;
		private Schema schema;
		private List<Field> fields = new List<Field>();
		private Dictionary<string, Field> fieldsByName = new Dictionary<string,Field>();
		private List<Constraint> constraints = new List<Constraint>();
		private Dictionary<string, Constraint> constraintsByName = new Dictionary<string,Constraint>();
		#endregion
	
		#region Private Methods
		private void SynchronizeFields()
		{
			foreach (Field field in fields)
			{
				if (!this.fieldsByName.ContainsKey(field.Name))
					this.fieldsByName.Add(field.Name, field);
			}
		}
		
		private void SynchronizeConstraints()
		{
			foreach (Constraint constraint in constraints)
			{
				if (!this.constraintsByName.ContainsKey(constraint.Name))
					this.constraintsByName.Add(constraint.Name, constraint);
			}
		}
		#endregion
	
		#region Public Properties
		public string Name
		{
			get { return name; }
		}

		public Schema Schema
		{
			get { return schema; }
		}

		public ReadOnlyCollection<Field> Fields
		{
			get { return fields.AsReadOnly(); }
		}

		public ReadOnlyCollection<Constraint> Constraints
		{
			get { return constraints.AsReadOnly(); }
		}
		#endregion

		#region Public Methods
		public Field AddField(string name, Type dataType)
		{
			return AddField(name, dataType, ConstraintType.None, null);
		}
		
		public Field AddField(string name, Type dataType, ConstraintType constraintType)
		{
			return AddField(name, dataType, constraintType);
		}
		
		public Field AddField(string name, Type dataType, ConstraintType constraintType, Predicate<object> predicate)
		{
			if (!string.IsNullOrEmpty(name) && dataType != null)
			{
				if (!fieldsByName.ContainsKey(name))
				{
					Field field = new Field(this, name, dataType);
					fields.Add(field);
					SynchronizeFields();
					
					switch (constraintType)
					{
						case ConstraintType.Identifier:
							AddConstraint(name + "_pk", ConstraintType.Identifier, field);
							break;
						case ConstraintType.Unique:
							AddConstraint(name + "_uq", ConstraintType.Unique, field);
							break;
						case ConstraintType.Validation:
							AddConstraint(name + "_ck", ConstraintType.Validation, predicate, field);
							break;
						default:
							break;
					}
					
					return field;
				}
			}
			
			return Field.Empty;
		}
		
		public Field GetField(string name)
		{
			if (fieldsByName.ContainsKey(name))
				return fieldsByName[name];
			else return Field.Empty;
		}

		public IList<Field> GetPrimaryKeyFields()
		{
			List<Field> primaryKeyFields = new List<Field>();
			
			foreach (Constraint constraint in Constraints)
			{
				if (constraint.Type == ConstraintType.Identifier)
				{
					foreach (Field field in constraint.Fields)
						primaryKeyFields.Add(field);
				}
			}
			
			return primaryKeyFields;
		}

		public Constraint AddConstraint(string name, ConstraintType type, params Field[] fields)
		{
			return AddConstraint(name, type, null, fields);
		}

		public Constraint AddConstraint(string name, ConstraintType type, Predicate<object> predicate, params Field[] fields)
		{
			if (!string.IsNullOrEmpty(name) && fields.Length > 0)
			{
				if (!constraintsByName.ContainsKey(name))
				{
					Constraint constraint = new Constraint(this, name, type, predicate, fields);
					constraints.Add(constraint);
					SynchronizeConstraints();
					return constraint;
				}
			}
			
			return Constraint.Empty;
		}

		public Constraint GetConstraint(string name)
		{
			if (constraintsByName.ContainsKey(name))
				return constraintsByName[name];
			else return Constraint.Empty;
		}
		#endregion		
	}
}
