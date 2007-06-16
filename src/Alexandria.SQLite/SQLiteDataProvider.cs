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
using Alexandria.Data;

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
		
		#region Test
		public string Test()
		{
			try
			{
				using (SQLiteConnection connection = GetSQLiteConnection(GetConnectionString()))
				{
					//connection.Open();
					//SQLiteCommand createCommand = new SQLiteCommand("CREATE TABLE AudioTrack (Id TEXT PRIMARY KEY, Location TEXT, Name TEXT, Album TEXT, Artist TEXT, Duration INT, ReleaseDate INT, TrackNumber INT, Format TEXT)", connection);
					//SQLiteCommand createCommand = new SQLiteCommand("CREATE TABLE MetadataId (Id TEXT PRIMARY KEY, ParentId TEXT, IdValue TEXT, IdType TEXT, IdVersion TEXT)", connection);
					//return createCommand.ExecuteNonQuery().ToString();
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("SQLite error", ex);
			}
			
			return string.Empty;
		}
		#endregion
		
		#endregion

		#region IDataStore Members
		public void Initialize(Type type)
		{
			TableMap map = mapFactory.CreateTableMap(this, type);
			map.CreateTables();
		}
		
		public T Lookup<T>(Guid id) where T : IPersistant
		{
			TableMap map = mapFactory.CreateTableMap(this, typeof(T));
			T record = map.LookupRecord<T>(id);
			//TODO: have the map do this...
			//if (record != null) record.DataStore = this;
			
			return record;
		}

		public void Save(IPersistant record)
		{
		}

		public void Delete(IPersistant record)
		{
		}
		#endregion
	}
}
