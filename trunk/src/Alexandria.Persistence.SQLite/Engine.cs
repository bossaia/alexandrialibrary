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
using System.Text;

using Telesophy.Alexandria.Persistence;

namespace Telesophy.Alexandria.Persistence.SQLite
{
	/*
	public class Engine : IEngine
	{
		#region Constructors
		public Engine()
		{
			if (ConfigurationManager.AppSettings[KEY_DB_DIR] != null)
				databaseDirectory = ConfigurationManager.AppSettings[KEY_DB_DIR].ToString();
			else databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + System.IO.Path.DirectorySeparatorChar + "Alexandria";

			if (!Directory.Exists(databaseDirectory))
				Directory.CreateDirectory(databaseDirectory);
				
			if (ConfigurationManager.AppSettings[KEY_DB_EXT] != null)
				databaseExtension = ConfigurationManager.AppSettings[KEY_DB_EXT].ToString();
			else databaseExtension = DEFAULT_DB_EXT;
		}
		#endregion

		#region Private Constants
		private const string KEY_DB_DIR = "DatabaseDirectory";
		private const string KEY_DB_EXT = "DatabaseExtension";
		private const string CON_STRING_FORMAT = "Data Source={0};New={1};UTF8Encoding=True;Version=3";
		private const string DEFAULT_DB_EXT = ".db";
		#endregion

		#region Private Fields
		private string databaseDirectory;
		private string databaseExtension;
		#endregion

		#region Private Methods
		private string GetDatabasePath(ISchema schema)
		{
			if (schema != null && !string.IsNullOrEmpty(schema.Name))
				return databaseDirectory + Path.DirectorySeparatorChar + schema.Name + databaseExtension;
			else throw new ArgumentException("Could not determine database path: schema is undefined");
		}
		
		private string GetConnectionString(ISchema schema)
		{
			string databasePath = GetDatabasePath(schema);
			bool databaseIsNew = (!File.Exists(databasePath));
			return string.Format(CON_STRING_FORMAT, databasePath, databaseIsNew);
		}

		private SQLiteConnection GetSQLiteConnection(ISchema schema)
		{
			return new System.Data.SQLite.SQLiteConnection(GetConnectionString(schema));
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

		private string GetOperatorName(Operator op)
		{
			switch (op)
			{
				case Operator.ContainedIn:
					return "IN";
				case Operator.EqualTo:
					return "=";
				case Operator.GreaterThan:
					return ">";
				case Operator.GreaterThanOrEqualTo:
					return ">=";
				case Operator.LessThan:
					return "<";
				case Operator.LessThanOrEqualTo:
					return "<=";
				case Operator.Like:
					return "LIKE";
				case Operator.NotEqualTo:
					return "<>";
				default:
					return string.Empty;
			}
		}

		private object GetDatabaseValue(Filter filter)
		{
			return GetDatabaseValue(filter.Field, filter.Value);
		}

		private object GetDatabaseValue(Field field, object value)
		{
			if (field.DataType != null)
			{
				string typeName = GetColumnTypeName(field.DataType);

				switch (typeName)
				{
					case "TEXT":
						if (value != null && value != DBNull.Value)
							return value.ToString();
						else return string.Empty;
					case "INTEGER":
						if (field.DataType == typeof(DateTime))
							return ((DateTime)value).ToFileTimeUtc();
						else if (field.DataType == typeof(TimeSpan))
							return ((TimeSpan)value).Ticks;
						else return Convert.ToInt32(value);
					case "REAL":
						return Convert.ToDecimal(value);
					default:
						break;
				}
			}
			return string.Empty;
		}

		private DateTime GetDateTime(object data)
		{
			try
			{
				return DateTime.FromFileTimeUtc(Convert.ToInt64(data));
			}
			catch
			{
				return default(DateTime);
			}
		}

		private TimeSpan GetTimeSpan(object data)
		{
			try
			{
				return TimeSpan.FromTicks(Convert.ToInt64(data));
			}
			catch
			{
				return default(TimeSpan);
			}
		}

		private Uri GetUri(object data)
		{
			try
			{
				return new Uri(data.ToString());
			}
			catch
			{
				return null;
			}
		}

		private Guid GetGuid(object data)
		{
			try
			{
				return new Guid(data.ToString());
			}
			catch
			{
				return default(Guid);
			}
		}
			
		private void AppendFields(StringBuilder text, IRecord record)
		{
			const string columnFormat = "{0} {1} NOT NULL";
			
			for (int fieldNumber = 0; fieldNumber < record.Fields.Count; fieldNumber++)
			{
				if (fieldNumber > 0) text.Append(", ");
				text.AppendFormat(columnFormat, record.Fields[fieldNumber].Name, GetColumnTypeName(record.Fields[fieldNumber].DataType));
			}
		}
		
		private void AppendConstraints(StringBuilder text, IRecord record)
		{
			bool appendFields = false;
		
			foreach (Constraint constraint in record.Constraints)
			{
				switch (constraint.Type)
				{
					case ConstraintType.Identifier:
						text.Append(", PRIMARY KEY (");
						appendFields = true;
						break;
					case ConstraintType.Unique:
						text.Append(", UNIQUE (");
						appendFields = true;
						break;
					default:
						break;
				}

				if (appendFields)
				{
					if (constraint.Fields.Count > 0)
					{
						for (int constraintFieldNumber = 0; constraintFieldNumber < constraint.Fields.Count; constraintFieldNumber++)
						{
							if (constraintFieldNumber > 0)
								text.Append(", ");

							text.Append(constraint.Fields[constraintFieldNumber].Name);
						}
					}
					
					text.Append(")");
				}
			}		
		}
		
		private void AppendLookupFields(StringBuilder text, IRecord record)
		{
			if (text != null)
			{
				for (int i=0; i<record.Fields.Count; i++)
				{
					if (i > 0) text.Append(", ");
					
					text.Append(record.Fields[i].Name);
				}
			}
		}
		
		private void AppendFilterFields(StringBuilder text, Query query, IDictionary<string, object> parameters)
		{
			if (text != null)
			{
				for (int i=0; i<query.Filters.Count; i++)
				{
					if (i > 0) text.Append(", ");
					
					string paramName = string.Format("@{0}", i);
					text.AppendFormat("{0} {1} {2}", query.Filters[i].Field.Name, GetOperatorName(query.Filters[i].Operator), paramName);
					parameters.Add(paramName, GetDatabaseValue(query.Filters[i]));
				}
			}
		}
		
		private ICommand GetCommand(string text, CommandFunction function)
		{
			return GetCommand(null, text, function);
		}
		
		private ICommand GetCommand(string type, string text, CommandFunction function)
		{
			return GetCommand(type, text, function, null);
		}
		
		private ICommand GetCommand(string type, string text, CommandFunction function, IDictionary<string, object> parameters)
		{
			return new Command(type, text, function, parameters);			
		}
		#endregion
	
		#region IEngine Members
		public ICommand GetInitializeSchemaCommand(ISchema schema)
		{
			return null;
		}

		public ICommand GetInitializeRecordCommand(IRecord record)
		{
			if (!string.IsNullOrEmpty(record.Name) && record.Fields != null && record.Fields.Count > 0)
			{
				const string format = "CREATE TABLE IF NOT EXISTS {0} ({1})";
				StringBuilder textBuilder = new StringBuilder();
				
				AppendFields(textBuilder, record);

				AppendConstraints(textBuilder, record);

				string text = string.Format(format, record.Name, textBuilder);
				
				return GetCommand(text, CommandFunction.Initialize);
			}
			
			return null;
		}

		public ICommand GetLookupCommand(string type, Query query)
		{
			if (query != null && query.Filters != null && query.Filters.Count > 0)
			{
				const string format = "SELECT {0} FROM {1} WHERE {2}";
				StringBuilder columns = new StringBuilder();
				StringBuilder filters = new StringBuilder();
				string recordName = query.Filters[0].Field.Record.Name;
				
				AppendLookupFields(columns, query.Filters[0].Field.Record);
		
				IDictionary<string, object> parameters = new Dictionary<string, object>();	
				AppendFilterFields(filters, query, parameters);
				
				string text = string.Format(format, columns, recordName, filters);
				return GetCommand(type, text, CommandFunction.Lookup, parameters);
			}
			
			return null;
		}

		public ICommand GetSaveCommand(Query query)
		{
			return null;
		}

		public ICommand GetSaveCommand(Tuple tuple)
		{
			return null;
		}

		public ICommand GetDeleteCommand(Query query)
		{
			return null;
		}

		public ICommand GetDeleteCommand(Tuple tuple)
		{
			return null;
		}

		public IResult Run(Batch batch)
		{
			return null;
		}
		#endregion
	}
	*/
}
