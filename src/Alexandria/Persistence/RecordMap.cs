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
using System.Data;

namespace Alexandria.Persistence
{
	public class RecordMap
	{
		#region Constructors
		public RecordMap(IPersistenceMechanism mechanism, ConstructorMap constructorMap)
		{
			this.mechanism = mechanism;
			this.constructorMap = constructorMap;
			
			this.dataRow = null;
		}

		public RecordMap(IPersistenceMechanism mechanism, ConstructorMap constructorMap, PropertyAttribute propertyAttribute, Guid id) : this(mechanism, constructorMap)
		{
			this.propertyAttribute = propertyAttribute;
			this.id = id;
			
			this.dataRow = null;
		}
		#endregion
		
		#region Private Fields
		private IPersistenceMechanism mechanism;
		private ConstructorMap constructorMap;
		private PropertyAttribute propertyAttribute;
		private Guid id;
		private IList<DataColumn> columns = new List<DataColumn>();
		private DataRow dataRow;
		#endregion
		
		#region Private Methods
		private object GetDatabaseValue(object value)
		{
			//return mechanism.GetDatabaseValue(value);
			return value;
		}
		
		private object GetRecordValue(object value)
		{
			//return mechanism.GetRecordValue(value);
			return value;
		}
		#endregion
		
		#region Public Properties
		public IPersistenceMechanism Mechanism
		{
			get { return mechanism; }
		}
				
		public ConstructorMap ConstructorMap
		{
			get { return constructorMap; }
		}

		public PropertyAttribute PropertyAttribute
		{
			get { return propertyAttribute; }
		}
		
		public Guid Id
		{
			get { return id; }
		}

		public IList<DataColumn> Columns
		{
			get { return columns; }
		}
		
		public DataRow DataRow
		{
			get { return dataRow; }
		}
		#endregion
		
		#region Public Methods
		public IRecord CreateRecord()
		{
			return null;
		}
		#endregion
	}
}
