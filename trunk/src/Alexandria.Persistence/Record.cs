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
			this.fields = new Dictionary<string, Field>();
			this.constraints = new Dictionary<string, Constraint>();
			this.primaryKeyFields = new List<Field>();
			
			if (fields != null)
			{
				foreach (Field field in fields)
				{
					if (!this.fields.ContainsKey(field.Name))
						this.fields.Add(field.Name, field);
				}
			}
			
			if (constraints != null)
			{
				foreach (Constraint constraint in constraints)
				{
					if (!this.constraints.ContainsKey(constraint.Name))
						this.constraints.Add(constraint.Name, constraint);

					if (constraint.Type == ConstraintType.Identifier)
						this.primaryKeyFields = constraint.Fields;
				}
			}
		}
		#endregion
	
		#region Private Fields
		private string name;
		private Schema schema;
		private Dictionary<string, Field> fields;
		private Dictionary<string, Constraint> constraints;
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

		public ICollection<Field> Fields
		{
			get { return (ICollection<Field>)fields.Values; }
		}

		public ICollection<Constraint> Constraints
		{
			get { return (ICollection<Constraint>)constraints.Values; }
		}
		
		public IList<Field> PrimaryKeyFields
		{
			get	{ return primaryKeyFields; }
		}
		#endregion

		#region Public Methods
		public Field GetField(string name)
		{
			if (fields.ContainsKey(name))
				return fields[name];
			else return Field.Empty;
		}

		public Constraint GetConstraint(string name)
		{
			if (constraints.ContainsKey(name))
				return constraints[name];
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
