#region License
/*
Copyright (c) 2007 Dan Poage

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[AttributeUsage(AttributeTargets.Interface)]
	public class RecordAttribute : Attribute
	{
		#region Constructors
		public RecordAttribute(string name)
		{
			this.name = name;
		}
		
		public RecordAttribute(string name, string idField, string recordTypeIdField) : this(name)
		{
			this.idField = idField;
			this.recordTypeIdField = recordTypeIdField;
		}
		#endregion
		
		#region Private Fields
		private string name;
		private string idField = "Id";
		private string recordTypeIdField = "RecordTypeId";
		#endregion
		
		#region Public Properties
		public string Name
		{
			get { return name; }
		}
		
		public string IdField
		{
			get { return idField; }
		}
		
		public string RecordTypeIdField
		{
			get { return recordTypeIdField; }
		}
		#endregion
	}
}
