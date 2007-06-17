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

namespace Alexandria.Persistance
{
	[AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct)]
	public class PersistanceClassAttribute : Attribute
	{
		#region Constructors
		public PersistanceClassAttribute()
		{
		}
		
		public PersistanceClassAttribute(string tableName, PersistanceLoadType loadType, string idFieldName)
		{
			this.tableName = tableName;
			this.loadType = loadType;
			this.idFieldName = idFieldName;
		}
		#endregion
		
		#region Private Fields
		private string tableName;
		private PersistanceLoadType loadType = PersistanceLoadType.None;
		private Type factoryType;
		private string factoryMethodName;
		private string idFieldName;
		private bool manuallySetProperties;
		#endregion
		
		#region Public Properties
		public string TableName
		{
			get { return tableName; }
			set { tableName = value; }
		}
		
		public PersistanceLoadType LoadType
		{
			get { return loadType; }
			set { loadType = value; }
		}
		
		public Type FactoryType
		{
			get { return factoryType; }
			set { factoryType = value; }
		}
		
		public string FactoryMethodName
		{
			get { return factoryMethodName; }
			set { factoryMethodName = value; }
		}
		
		public string IdFieldName
		{
			get { return idFieldName; }
			set { idFieldName = value; }
		}
		
		public bool ManuallySetProperties
		{
			get { return manuallySetProperties; }
			set { manuallySetProperties = value; }
		}
		#endregion
	}
}
