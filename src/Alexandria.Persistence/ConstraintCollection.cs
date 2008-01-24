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
	public class ConstraintCollection : NamedItemCollectionBase<Constraint>
	{
		#region Constructors
		public ConstraintCollection(IRecord record)
		{
			this.record = record;
		}
		#endregion
		
		#region Private Fields
		private IRecord record;
		#endregion
		
		#region Public Properties
		public IRecord Record
		{
			get { return record; }
		}
		#endregion
		
		#region Public Methods
		public void Add(string name, ConstraintType type, params Field[] fields)
		{
			Add(name, type, null, fields);
		}

		public void Add(string name, ConstraintType type, Predicate<object> predicate, params Field[] fields)
		{
			if (!string.IsNullOrEmpty(name) && fields.Length > 0)
			{
				if (!base.Contains(name))
				{
					Constraint constraint = new Constraint(record, name, type, predicate, fields);
					base.InsertItem(base.Count, constraint);
				}
			}
		}
		#endregion
	}
}
