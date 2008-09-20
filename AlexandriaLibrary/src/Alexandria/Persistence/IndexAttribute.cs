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
	public class IndexAttribute : Attribute
	{
		#region Constructors
		public IndexAttribute(string indexName)
		{
			this.indexName = indexName;
		}
		
		public IndexAttribute(string indexName, bool isUnique) : this(indexName)
		{
			this.isUnique = isUnique;
		}
		
		public IndexAttribute(string indexName, bool isUnique, int ordinal) : this(indexName, isUnique)
		{
			this.ordinal = ordinal;
		}
		#endregion
		
		#region Private Fields
		private string indexName;
		private bool isUnique;		
		private int ordinal;
		#endregion
		
		#region Public Properties
		public string IndexName
		{
			get { return indexName; }
		}
		
		public bool IsUnique
		{
			get { return isUnique; }
		}
		
		public int Ordinal
		{
			get { return ordinal; }
		}
		#endregion
	}
}
