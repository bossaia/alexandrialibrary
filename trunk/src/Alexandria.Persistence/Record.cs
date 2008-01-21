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

namespace Telesophy.Alexandria.Persistence
{
	public struct Record
	{
		#region Constructors
		public Record(string name, Schema schema, IList<Field> fields, IList<Constraint> constraints)
		{
			this.name = name;
			this.schema = schema;
			this.fields = new List<Field>();
			this.fieldsByName = new Dictionary<string, Field>();
			this.constraints = new List<Constraint>();
			this.constraintsByName = new Dictionary<string, Constraint>();
			this.primaryKeyFields = new List<Field>();
			
			if (fields != null)
			{
				this.fields = fields;
			
				foreach (Field field in fields)
				{
					if (!this.fieldsByName.ContainsKey(field.Name))
						this.fieldsByName.Add(field.Name, field);
				}
			}
			
			if (constraints != null)
			{
				this.constraints = constraints;
			
				foreach (Constraint constraint in constraints)
				{
					if (!this.constraintsByName.ContainsKey(constraint.Name))
						this.constraintsByName.Add(constraint.Name, constraint);

					if (constraint.Type == ConstraintType.Identifier)
						this.primaryKeyFields = constraint.Fields;
				}
			}
		}
		#endregion
	
		#region Private Fields
		private string name;
		private Schema schema;
		private IList<Field> fields;
		private Dictionary<string, Field> fieldsByName;
		private IList<Constraint> constraints;
		private Dictionary<string, Constraint> constraintsByName;
		private IList<Field> primaryKeyFields;
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

		public IList<Field> Fields
		{
			get { return fields; }
		}

		public IList<Constraint> Constraints
		{
			get { return constraints; }
		}
		
		public IList<Field> PrimaryKeyFields
		{
			get	{ return primaryKeyFields; }
		}
		#endregion

		#region Public Methods
		public Field GetField(string name)
		{
			if (fieldsByName.ContainsKey(name))
				return fieldsByName[name];
			else return Field.Empty;
		}

		public Constraint GetConstraint(string name)
		{
			if (constraintsByName.ContainsKey(name))
				return constraintsByName[name];
			else return Constraint.Empty;
		}
		#endregion
		
		#region Static Members
		private static Record empty = new Record();
		
		public static Record Empty
		{
			get { return empty; }
		}
		#endregion
	}
}
