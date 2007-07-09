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
using Alexandria.Persistence;

namespace Alexandria.SQLite
{
	public class SQLitePersistenceMechanism : IPersistenceMechanism
	{
		#region Constructors
		public SQLitePersistenceMechanism(string databasePath)
		{
			this.databasePath = databasePath;
		}
		#endregion

		#region Private Constant Fields
		private const string RECORD_TYPE_ID = "RecordTypeId";
		#endregion

		#region Private Fields
		string databasePath;
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

		#region NormalizeFromDatabaseValue
		private object NormalizeFromDatabaseValue(Type type, object value)
		{
			if (type == typeof(int))
			{
				if (value == DBNull.Value)
					return 0;
			}
			if (type == typeof(DateTime))
			{
				if (value == DBNull.Value)
					return DateTime.MinValue;
				else return Convert.ToDateTime(value.ToString());
			}
			else if (type == typeof(TimeSpan))
			{
				if (value == null || value == DBNull.Value)
					return TimeSpan.Zero;
				else return new TimeSpan(0, 0, 0, 0, Convert.ToInt32(value));
			}
			else if (type == typeof(Version))
			{
				return new Version(value.ToString());
			}
			else if (type == typeof(Uri))
			{
				return new Uri(value.ToString());
			}

			return value;
		}
		#endregion

		#region NormalizeToDatabaseValue
		private string NormalizeToDatabaseValue(Type type, object value)
		{
			if (value == DBNull.Value || value == null)
				return "NULL";

			if (type == typeof(short) || type == typeof(int) || type == typeof(long))
			{
				return value.ToString();
			}
			else if (type == typeof(decimal) || type == typeof(float) || type == typeof(double))
			{
				return value.ToString();
			}
			else if (type == typeof(Guid))
			{
				return string.Format("'{0}'", value.ToString().ToLowerInvariant());
			}
			else if (type == typeof(DateTime))
			{
				DateTime date = (DateTime)value;
				if (date == DateTime.MinValue)
					return "NULL";
				else return date.ToFileTime().ToString();
			}
			else if (type == typeof(TimeSpan))
			{
				TimeSpan span = (TimeSpan)value;
				return span.TotalMilliseconds.ToString();
			}
			else if (type == typeof(Version))
			{
				return string.Format("'{0}'", value);
			}
			else if (type == typeof(Uri))
			{
				return string.Format("'{0}'", value);
			}

			return string.Format("'{0}'", value);
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

		#region IPersistenceMechanism Members
		public string Name
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool IsOpen
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Open()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Close()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public DataTable GetDataTable(IPersistenceBroker broker, Type type)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void FillDataTable(DataTable table, string idValue)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object GetDatabaseValue(object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object GetRecordValue(object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
