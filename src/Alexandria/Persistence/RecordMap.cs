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
		public RecordMap(IPersistenceMechanism mechanism, DataRow data)
		{
			this.mechanism = mechanism;
			this.data = data;
			this.constructorMap = GetConstructorMap();
			//this.idField = idField;
			//this.idValue = idValue;
		}
		
		public RecordMap(IPersistenceMechanism mechanism, DataRow data, PropertyMap propertyMap) : this(mechanism, data)
		{
			this.propertyMap = propertyMap;
		}
		#endregion
		
		#region Private Fields
		private IPersistenceMechanism mechanism;
		private DataRow data;
		private PropertyMap propertyMap;
		private ConstructorMap constructorMap;
		private IList<RecordMap> Children = new List<RecordMap>();
		private string idField;
		private string idValue;
		private IRecord record;
		#endregion
		
		#region Private Methods
		private ConstructorMap GetConstructorMap()
		{
			return new ConstructorMap(null, null);
		}
		
		private IDictionary<string, object> GetConstructorParameterMap()
		{
			if (data != null)
			{
				IDictionary<string, object> parameterMap = new Dictionary<string, object>();
				for(int i=0;i<data.ItemArray.Length;i++)
				{
					//NOTE: data[i] should already be normalize by the mechanism
					parameterMap.Add(data.Table.Columns[i].ColumnName, data[i]);
				}
				return parameterMap;
			}
			else throw new InvalidOperationException("data is undefined");
		}
		#endregion
		
		#region Public Properties
		public IPersistenceMechanism Mechanism
		{
			get { return mechanism; }
		}
		
		public DataRow Data
		{
			get { return data; }
		}

		public PropertyMap PropertyMap
		{
			get { return propertyMap; }
		}

		public ConstructorMap ConstructorMap
		{
			get { return constructorMap; }
		}

		public string IdField
		{
			get { return idField; }
		}
		
		public string IdValue
		{
			get { return idValue; }
		}
				
		public IRecord Record
		{
			get { return record; }
		}
		#endregion
		
		#region Public Methods
		public void Lookup()
		{
		}
		#endregion
	}
}
