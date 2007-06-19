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
using System.Collections;
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
		private ClassAttribute GetClassAttribute(Type type)
		{
			foreach (ClassAttribute attribute in type.GetCustomAttributes(typeof(ClassAttribute), false))
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
				foreach (ConstructorAttribute attribute in constructor.GetCustomAttributes(typeof(ConstructorAttribute), false))
				{
					return constructor;
				}
			}
			return null;
		}
		#endregion
		
		#region GetArrayFromDictionary
		private object[] GetArrayFromDictionary(IDictionary<int, object> dictionary)
		{
			object[] array = new object[dictionary.Count];
			for(int i=0;i<dictionary.Count;i++)
			{
				array[i] = dictionary[i+1];
			}
			
			return array;
		}
		#endregion
		
		#endregion
		
		#region Internal Methods
		internal TableMap CreateTableMap(IMappingStrategy strategy, Type type)
		{
			TableMap map = null;
		
			ConstructorInfo constructor = GetConstructor(type);
			ClassAttribute classAttribute = GetClassAttribute(type);

			if (classAttribute != null)
			{
				string tableName = classAttribute.TableName;

				if (!string.IsNullOrEmpty(tableName))
				{
					DataTable table = new DataTable(tableName);
					map = new TableMap(strategy, type, table, classAttribute, constructor);
					
					IDictionary<int, DataColumn> columns = new Dictionary<int, DataColumn>();
					
					IDictionary<int, object> data = new Dictionary<int, object>();
					IList<Dictionary<int, object>> dataCollections = new List<Dictionary<int, object>>();
					if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
					{
						if (strategy.Type == MappingType.Collection)
						{
							foreach(IPersistant p in strategy.Records)
								dataCollections.Add(new Dictionary<int, object>());
						}
					}
					
					int i = 1; int ordinal;

					foreach (PropertyInfo property in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
					{
						foreach (PropertyAttribute attribute in property.GetCustomAttributes(typeof(PropertyAttribute), false))
						{
							if (attribute.FieldType == FieldType.Basic)
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
								
								if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
								{
									if (strategy.Type == MappingType.Singleton)
									{
										data[ordinal] = property.GetValue(strategy.Record, null);
									}
									else if (strategy.Type == MappingType.Collection)
									{
										for(int k=0;k<strategy.Records.Count;k++)
											dataCollections[k].Add(ordinal, property.GetValue(strategy.Records[k], null));
									}
								}
							}
							else
							{
								PropertyMap propertyMap = new PropertyMap(property, attribute);
								TableMap childMap = null;
								
								if (attribute.FieldType == FieldType.OneToOneChild)
								{
									IMappingStrategy childStrategy = strategy;
									if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
									{
										if (strategy.Type == MappingType.Singleton)
										{
											IPersistant childRecord = (IPersistant)property.GetValue(strategy.Record, null);
											childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, childRecord);
										}
										else if (strategy.Type == MappingType.Collection)
										{
											IList<IPersistant> records = new List<IPersistant>();
											foreach(IPersistant child in strategy.Records)
											{
												IPersistant childRecord = (IPersistant)property.GetValue(child, null);
												records.Add(childRecord);
											}
											childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, records);
										}
									}
									childMap = CreateTableMap(childStrategy, property.PropertyType);
								}
								else if (attribute.FieldType == FieldType.OneToManyChildren && attribute.ChildType != null)
								{
									IMappingStrategy childStrategy = strategy;
									if (strategy.Function == MappingFunction.Save || strategy.Function == MappingFunction.Delete)
									{
										if (strategy.Type == MappingType.Singleton)
										{
											IList childObjects = null;
											List<IPersistant> records = new List<IPersistant>();
											childObjects = (IList)property.GetValue(strategy.Record, null);
											foreach(IPersistant persistant in childObjects)
												records.Add(persistant);
											childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, records);
										}
										else if (strategy.Type == MappingType.Collection)
										{
											IList<IPersistant> records = new List<IPersistant>();
											foreach (IPersistant childRecord in strategy.Records)
											{
												IList childObjects = null;
												childObjects = (IList)property.GetValue(childRecord, null);
												foreach(IPersistant persistant in childObjects)
													records.Add(persistant);
											}
											childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, records);
										}
									}
									childMap = CreateTableMap(childStrategy, attribute.ChildType);
								}
								else throw new ApplicationException("Could not map this property: invalid field type");

								map.Children.Add(propertyMap, childMap);
							}
						}
					}

					if (columns.Count > 0)
					{
						for (int j = 1; j <= columns.Count; j++)
							table.Columns.Add(columns[j]);
							
						if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
						{
							if (strategy.Type == MappingType.Singleton)
							{
								object[] dataRow = GetArrayFromDictionary(data);
								table.Rows.Add(dataRow);
							}
							else if (strategy.Type == MappingType.Collection)
							{
								foreach(Dictionary<int, object> dataCollection in dataCollections)
								{
									object[] dataRow = GetArrayFromDictionary(dataCollection);
									table.Rows.Add(dataRow);
								}
							}
						}
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
