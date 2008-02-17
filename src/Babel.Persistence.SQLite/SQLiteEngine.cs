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
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence.SQLite
{
	public class SQLiteEngine : IEngine
	{
		#region Constructors
		public SQLiteEngine()
		{
			if (ConfigurationManager.AppSettings[KEY_DB_DIR] != null)
				databaseDirectory = ConfigurationManager.AppSettings[KEY_DB_DIR].ToString();
			else databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + System.IO.Path.DirectorySeparatorChar + "Alexandria";

			if (!Directory.Exists(databaseDirectory))
				Directory.CreateDirectory(databaseDirectory);
		}
		#endregion
		
		#region Private Constants
		private const string KEY_DB_DIR = "DatabaseDirectory";
		private const string DATABASE_EXT = ".db";
		private const string CON_STRING_FORMAT = "Data Source={0};New={1};UTF8Encoding=True;Version=3";
		private const string TABLE_FORMAT = "CREATE TABLE IF NOT EXISTS {0} ({1})";
		private const string COLUMN_FORMAT = "{0} {1} {2}";
		private const string LIST_SEPARATOR = ", ";
		#endregion
			
		#region Private Fields
		private string databaseDirectory;
		#endregion
		
		#region Private Methods
		private string GetConnectionString(ISchema schema)
		{
			if (schema != null)
			{
				string name = schema.Name + DATABASE_EXT;
				string path = Path.Combine(databaseDirectory, name);
				bool isNew = false;
				isNew = (!File.Exists(path));
				return string.Format(CON_STRING_FORMAT, path, isNew);
			}
			
			return null;
		}

		private SQLiteConnection GetConnection(ISchema schema)
		{
			return new SQLiteConnection(GetConnectionString(schema));
		}

		private void InitializeMap(IMap map, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			if (map != null)
			{
				StringBuilder builder = new StringBuilder();
				for (int count = 0; count < map.Fields.Count; count++)
				{
					if (count > 0) builder.Append(LIST_SEPARATOR);
					string name = map.Fields[count].Name;
					string type = GetColumnTypeName(map.Fields[count].Type);
					string constraints = GetColumnConstraints(map.Fields[count]);
					builder.AppendFormat(COLUMN_FORMAT, name, type, constraints);
				}

				//foreach (System.Data.Constraint constraint in table.Constraints)
				//{
				//    if (constraint is UniqueConstraint)
				//    {
				//        UniqueConstraint uniqueConstraint = constraint as UniqueConstraint;
				//        if (uniqueConstraint.IsPrimaryKey)
				//            createTableText.Append(", PRIMARY KEY (");
				//        else createTableText.Append(", UNIQUE (");

				//        for (int constraintColumnNumber = 0; constraintColumnNumber < uniqueConstraint.Columns.Length; constraintColumnNumber++)
				//        {
				//            if (constraintColumnNumber > 0)
				//                createTableText.Append(", ");

				//            createTableText.Append(uniqueConstraint.Columns[constraintColumnNumber].ColumnName);
				//        }

				//        createTableText.Append(")");
				//    }
				//}

				string commandText = string.Format(TABLE_FORMAT, map.Name, builder);
				SQLiteCommand command = GetCommand(commandText, connection, transaction);
				command.ExecuteNonQuery();
			}
		}

		private SQLiteCommand GetCommand(string commandText, SQLiteConnection connection, SQLiteTransaction transaction)
		{
			SQLiteCommand command = new SQLiteCommand(commandText, connection, transaction);
			command.CommandType = CommandType.Text;
			return command;
		}

		private string GetColumnTypeName(Type type)
		{
			string name = "TEXT";

			if (type == typeof(bool) ||
				type == typeof(sbyte) ||
				type == typeof(byte) ||
				type == typeof(short) ||
				type == typeof(ushort) ||
				type == typeof(int) ||
				type == typeof(uint) ||
				type == typeof(long) ||
				type == typeof(ulong) ||
				type == typeof(DateTime) ||
				type == typeof(TimeSpan))
				name = "INTEGER";

			if (type == typeof(float) ||
				type == typeof(double) ||
				type == typeof(decimal))
				name = "REAL";

			return name;
		}

		private string GetColumnConstraints(Field field)
		{
			if (field != Field.Empty)
			{
			
			}
			
			return string.Empty;
		}

		private object GetValue(DataRow row, int index)
		{
			if (row != null && index >= 0 && index < row.Table.Columns.Count)
			{
				DataColumn col = row.Table.Columns[index];
				string typeName = GetColumnTypeName(col.DataType);

				switch (typeName)
				{
					case "TEXT":
						if (row[index] != null && row[index] != DBNull.Value)
							return row[index].ToString();
						else return string.Empty;
					case "INTEGER":
						if (col.DataType == typeof(bool))
							return Convert.ToBoolean((int)row[index]);
						else if (col.DataType == typeof(DateTime))
							return ((DateTime)row[index]).ToFileTimeUtc();
						else if (col.DataType == typeof(TimeSpan))
							return ((TimeSpan)row[index]).Ticks;
						else
							return Convert.ToInt32(row[index]);
					case "REAL":
						return Convert.ToDecimal(row[index]);
					default:
						break;
				}
			}
			return string.Empty;
		}		
		#endregion
		
		#region INamedItem Members
		public string Name
		{
			get { return "SQLite Database Engine"; }
		}
		#endregion
	
		#region IEngine Members
		public void Initialize(ISchema schema)
		{
			if (schema != null)
			{
				SQLiteConnection connection = null;
				SQLiteTransaction transaction = null;
				try
				{
					connection = GetConnection(schema);
					connection.Open();
					transaction = connection.BeginTransaction(false);
					
					foreach (IMap map in schema.Maps)
					{
						InitializeMap(map, connection, transaction);
					}
					
					transaction.Commit();
				}
				catch (Exception ex)
				{
					if (transaction != null)
						transaction.Rollback();
						
					throw ex;
				}
				finally
				{
					if (connection != null && connection.State != ConnectionState.Closed)
						connection.Close();
				}
			}
		}

		public IResult Lookup(IMap map, Query query)
		{
			throw new NotImplementedException();
		}

		public void Save(IMap map, object model)
		{
			throw new NotImplementedException();
		}

		public void Delete(IMap map, object model)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
