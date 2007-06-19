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
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using Alexandria;
using Alexandria.Persistance;

namespace Alexandria.SQLite
{
	public class SQLiteDataProvider : IDataStore
	{			
		#region Constructors
		public SQLiteDataProvider(string databasePath)
		{		
			this.databasePath = databasePath;
		}
		#endregion
				
		#region Private Fields
		string databasePath;
		private TableMapFactory mapFactory = new TableMapFactory();
		#endregion
		
		#region Private Methods
		
		#region GetConnectionString
		private string GetConnectionString()
		{
			bool databaseIsNew = false;
			databaseIsNew = (!File.Exists(databasePath));
			return string.Format("Data Source={0};New={1};UTF8Encoding=True;Version=3", databasePath, databaseIsNew);		
		}
		#endregion
		
		#endregion
	
		#region Internal Methods
		
		#region GetSQLiteConnection
		internal SQLiteConnection GetSQLiteConnection()
		{
			return GetSQLiteConnection(GetConnectionString());
		}

		internal SQLiteConnection GetSQLiteConnection(string connectionString)
		{
			return new SQLiteConnection(connectionString);
		}
		#endregion		
				
		#endregion

		#region IDataStore Members
		public void Initialize(Type type)
		{
			//IMappingStrategy strategy = new MappingStrategy(this, MappingFunction.Initialize);
			//strategy, type);
			TableMap map = mapFactory.CreateTableMap(this, MappingFunction.Initialize, type);
			map.Initialize();
		}
		
		public T Lookup<T>(Guid id) where T : IPersistant
		{
			//IMappingStrategy strategy = new MappingStrategy(this, MappingFunction.Lookup);
			//strategy, typeof(T));
			TableMap map = mapFactory.CreateTableMap(this, MappingFunction.Lookup, typeof(T));
			return map.Lookup<T>(id);
		}

		public void Save(IPersistant record)
		{
			//IMappingStrategy strategy = new MappingStrategy(this, MappingFunction.Save, record);
			//strategy, record.GetType());
			TableMap map = mapFactory.CreateTableMap(this, MappingFunction.Save, record);
			map.Save();
		}

		public void Delete(IPersistant record)
		{
			//IMappingStrategy strategy = new MappingStrategy(this, MappingFunction.Delete, record);
			//strategy, record.GetType());
			TableMap map = mapFactory.CreateTableMap(this, MappingFunction.Delete, record);
			map.Delete();
		}
		#endregion
	}
}
