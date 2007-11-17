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
using System.Data;
using System.Reflection;
using System.Text;

namespace Telesophy.Alexandria.Persistence
{
	public abstract class DataMap<T> : IDataMap<T>
	{
		private PropertyInfo GetPropertyByName(object record, string name)
		{
			if (record != null && !string.IsNullOrEmpty(name))
				return record.GetType().GetProperty(name);
			else return null;
		}
	
		#region IDataMap<T> Members
		public abstract DataTable Table { get; }

		public virtual void Load(IEngine engine, T record, Uri id)
		{
			using (IDbCommand command = engine.GetLoadCommand(Table.TableName, Table.PrimaryKey[0].ColumnName, id.ToString()))
			{
				//command.CommandText = string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", Table.TableName, Table.PrimaryKey[0].ColumnName, id);
				using (IDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						for(int i=0; i<Table.Columns.Count; i++)
						{
							if (!reader.IsDBNull(i))
							{
								PropertyInfo property = GetPropertyByName(record, Table.Columns[i].ColumnName);
								if (property != null)
								{
									property.SetValue(record, reader[i], null);
								}
							}
						}
					}
				}
			}
		}

		public virtual void Save(IEngine engine, T record, Uri id)
		{
			IDictionary<string, string> fieldValuePairs = new Dictionary<string, string>();
			for(int i=0; i < Table.Columns.Count; i++)
			{
				PropertyInfo property = GetPropertyByName(record, Table.Columns[i].ColumnName);
				if (property != null)
				{
					object value = property.GetValue(record, null);
					string valueName = "''";
					if (property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime))
					{
						if (value != null) valueName = string.Format("'{0}'", value);
					}
					else valueName = value.ToString();
					
					fieldValuePairs.Add(Table.Columns[i].ColumnName, value.ToString());
				}
			}
			
			if (fieldValuePairs.Count > 0)
			{
				using (IDbCommand command = engine.GetSaveCommand(Table.TableName, Table.PrimaryKey[0].ColumnName, id.ToString(), fieldValuePairs))
				{
					command.ExecuteNonQuery();
				}
			}
		}

		public virtual void Delete(IEngine engine, T record, Uri id)
		{
			using(IDbCommand command = engine.GetDeleteCommand(Table.TableName, Table.PrimaryKey[0].ColumnName, id.ToString()))
			{
				command.ExecuteNonQuery();
			}
		}
		#endregion
	}
}
