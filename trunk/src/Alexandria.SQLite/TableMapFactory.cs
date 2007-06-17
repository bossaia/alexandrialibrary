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
using System.Reflection;
using System.Text;
using Alexandria.Persistance;

namespace Alexandria.SQLite
{
	internal class TableMapFactory
	{
		#region Constructors
		internal TableMapFactory()
		{
		}
		#endregion

		#region Private Methods

		#region GetClassAttribute
		private PersistanceClassAttribute GetClassAttribute(Type type)
		{
			foreach (PersistanceClassAttribute attribute in type.GetCustomAttributes(typeof(PersistanceClassAttribute), false))
			{
				return attribute;
			}
			return null;
		}
		#endregion

		#region GetConstructor
		private ConstructorInfo GetConstructor(Type type)
		{
			foreach (ConstructorInfo constructor in type.GetConstructors()) // (BindingFlags.Public))
			{
				foreach (PersistanceConstructorAttribute attribute in constructor.GetCustomAttributes(typeof(PersistanceConstructorAttribute), false))
				{
					return constructor;
				}
			}
			return null;
		}
		#endregion
		
		#endregion
		
		#region Internal Methods
		internal TableMap CreateTableMap(SQLiteDataProvider provider, Type type)
		{
			TableMap map = null;
		
			ConstructorInfo constructor = GetConstructor(type);
			PersistanceClassAttribute classAttribute = GetClassAttribute(type);

			if (classAttribute != null)
			{
				string tableName = classAttribute.TableName;

				if (!string.IsNullOrEmpty(tableName))
				{
					DataTable table = new DataTable(tableName);
					map = new TableMap(provider, type, table, classAttribute, constructor);

					IDictionary<int, DataColumn> columns = new Dictionary<int, DataColumn>();
					int i = 1; int ordinal;

					foreach (PropertyInfo property in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
					{
						foreach (PersistancePropertyAttribute attribute in property.GetCustomAttributes(typeof(PersistancePropertyAttribute), false))
						{
							if (attribute.FieldType == PersistanceFieldType.Basic)
							{
								ordinal = (attribute.Ordinal > 0) ? attribute.Ordinal : i;
								DataColumn column = new DataColumn(property.Name, property.PropertyType);
								column.Unique = attribute.IsUnique;
								column.AllowDBNull = !attribute.IsRequired;
								column.DefaultValue = attribute.DefaultValue;
								if (attribute.MaxLength > 0)
									column.MaxLength = attribute.MaxLength;

								columns.Add(ordinal, column);
								i++;
							}
							else
							{
								PropertyMap propertyMap = new PropertyMap(property, attribute);
								TableMap childMap = null;
								
								if (attribute.FieldType == PersistanceFieldType.OneToOneChild)
									childMap = CreateTableMap(provider, property.PropertyType);
								else if (attribute.FieldType == PersistanceFieldType.OneToManyChildren && attribute.ChildType != null)
									childMap = CreateTableMap(provider, attribute.ChildType);
								else throw new ApplicationException("Could not map this property: invalid field type");

								map.Children.Add(propertyMap, childMap);
							}
						}
					}

					if (columns.Count > 0)
					{
						for (int j = 1; j <= columns.Count; j++)
							table.Columns.Add(columns[j]);
					}
					else throw new ApplicationException("Could not find any columns for type: " + type.Name);

				}
				else throw new ApplicationException("Could not map this type to a DataTable: table name undefined");
			}
			else throw new ApplicationException("Could not map this type to a DataTable: class persistance attribute not found");
			
			return map;
		}
		#endregion
	}
}
