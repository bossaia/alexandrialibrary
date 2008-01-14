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
	public struct Constraint
	{
		#region Constructors
		public Constraint(string name, ConstraintType type, params Field[] fields) : this(name, type, null, fields)
		{
		}
		
		public Constraint(string name, ConstraintType type, Predicate<object> predicate, params Field[] fields)
		{
			this.name = name;
			this.type = type;

			this.fields = new List<Field>();
			foreach (Field field in fields)
				this.fields.Add(field);

			this.predicate = predicate;
		}
		#endregion
	
		#region Private Fields
		private string name;
		private ConstraintType type;
		private IList<Field> fields;
		private Predicate<object> predicate;
		#endregion
	
		#region Public Properties
		public string Name
		{
			get { return name; }
		}

		public ConstraintType Type
		{
			get { return type; }
		}

		public IList<Field> Fields
		{
			get { return fields; }
		}

		public Predicate<object> Predicate
		{
			get { return predicate; }
		}
		#endregion
	}
}
