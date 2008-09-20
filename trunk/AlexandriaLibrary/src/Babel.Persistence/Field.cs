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
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public class Field : NamedItem
	{
		#region Constructors
		public Field(string name, Entity parent, Type type)
			: base(name)
		{
			this.parent = parent;
			this.type = type;
		}

		public Field(string name, Entity parent, Type type, bool isRequired)
			: this(name, parent, type)
		{
			this.isRequired = isRequired;
		}

		public Field(string name, Entity parent, Type type, bool isRequired, bool isUnique)
			: this(name, parent, type, isRequired)
		{
			this.isUnique = isUnique;
		}

		public Field(string name, Entity parent, Type type, bool isRequired, bool isUnique, bool isHidden)
			: this(name, parent, type, isRequired, isUnique)
		{
			this.isHidden = isHidden;
		}
		#endregion
		
		#region Private Fields
		private Entity parent;
		private Type type;
		private bool isRequired = false;
		private bool isUnique = false;
		private bool isHidden = false;
		#endregion
		
		#region Public Properties
		public Entity Parent
		{
			get { return parent; }
		}
		
		public Type Type
		{
			get { return type; }
		}
		
		public bool IsRequired
		{
			get { return isRequired; }
		}
		
		public bool IsUnique
		{
			get { return isUnique; }
		}
		
		public bool IsHidden
		{
			get { return isHidden; }
		}
		#endregion
		
		#region Public Overrides
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Field)
			{
				Field other = (Field)obj;
				return this.ToString().Equals(other.ToString());
			}
			
			return false;
		}
		
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}", Parent.Name, Name);
		}
		#endregion
	}
}
