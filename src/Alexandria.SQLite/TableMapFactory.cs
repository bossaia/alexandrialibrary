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
using Alexandria.Persistence;

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
		
		#region AddColumn
		private int AddColumn(int count, PropertyInfo property, PropertyAttribute attribute, IDictionary<int, DataColumn> columns, IDictionary<int, object> data, IList<Dictionary<int, object>> dataCollections, IMappingStrategy strategy)
		{			
			int ordinal = (attribute.Ordinal > 0) ? attribute.Ordinal : count;
			string fieldName = (!string.IsNullOrEmpty(attribute.FieldName)) ? attribute.FieldName : property.Name;
			Type storedType = (attribute.StoreType == StoreType.Id) ? typeof(Guid) : property.PropertyType;
			DataColumn column = new DataColumn(fieldName, storedType);
			column.Unique = attribute.IsUnique;
			column.AllowDBNull = !attribute.IsRequired;
			column.DefaultValue = attribute.DefaultValue;
			if (attribute.MaxLength > 0)
				column.MaxLength = attribute.MaxLength;

			columns.Add(ordinal, column);
			count++;

			if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
			{
				if (strategy.Type == MappingType.Singleton)
				{
					data[ordinal] = property.GetValue(strategy.Record, null);
				}
				else if (strategy.Type == MappingType.Collection)
				{
					for (int i = 0; i < strategy.Records.Count; i++)
						dataCollections[i].Add(ordinal, property.GetValue(strategy.Records[i], null));
				}
			}
			
			return count;
		}
		#endregion
		
		#region GetChildMap
		private TableMap GetChildMap(PropertyInfo property, PropertyAttribute attribute, IMappingStrategy strategy)
		{
			IMappingStrategy childStrategy = strategy;

			if (attribute.FieldType == FieldType.OneToOneChild || attribute.FieldType == FieldType.ManyToOneChild)
			{				
				if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
				{
					if (strategy.Type == MappingType.Singleton)
					{
						IPersistent childRecord = (IPersistent)property.GetValue(strategy.Record, null);
						childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, childRecord);
					}
					else if (strategy.Type == MappingType.Collection)
					{
						IList<IPersistent> records = new List<IPersistent>();
						foreach (IPersistent child in strategy.Records)
						{
							IPersistent childRecord = (IPersistent)property.GetValue(child, null);
							records.Add(childRecord);
						}
						childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, records);
					}
				}
				return CreateTableMap(childStrategy, property.PropertyType, attribute.CascadeSave, attribute.CascadeDelete);
			}
			else if (attribute.FieldType == FieldType.OneToManyChildren)
			{				
				if (strategy.Function == MappingFunction.Save || strategy.Function == MappingFunction.Delete)
				{
					if (strategy.Type == MappingType.Singleton)
					{
						IList childObjects = null;
						List<IPersistent> records = new List<IPersistent>();
						childObjects = (IList)property.GetValue(strategy.Record, null);
						foreach (IPersistent persistant in childObjects)
							records.Add(persistant);
						childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, records);
					}
					else if (strategy.Type == MappingType.Collection)
					{
						IList<IPersistent> records = new List<IPersistent>();
						foreach (IPersistent childRecord in strategy.Records)
						{
							IList childObjects = null;
							childObjects = (IList)property.GetValue(childRecord, null);
							foreach (IPersistent persistant in childObjects)
								records.Add(persistant);
						}
						childStrategy = new MappingStrategy(strategy.Provider, strategy.Function, records);
					}
				}
				//return CreateTableMap(childStrategy, attribute.ChildType, attribute.CascadeSave, attribute.CascadeDelete);
				//TODO: Fix this so that it can dynamically determine what the child type is
				return null;				
			}
			else throw new ApplicationException("Could not get a child map for this property: invalid field type");
		}
		#endregion
		
		#endregion
				
		#region Internal Methods
		internal TableMap CreateTableMap(SQLiteDataProvider provider, MappingFunction function, Type type)
		{
			IMappingStrategy strategy = new MappingStrategy(provider, function);
			return CreateTableMap(strategy, type);
		}

		internal TableMap CreateTableMap(SQLiteDataProvider provider, MappingFunction function, IPersistent record)
		{
			if (record != null)
			{
				IMappingStrategy strategy = new MappingStrategy(provider, function, record);
				return CreateTableMap(strategy, record.GetType());
			}
			else throw new ArgumentNullException("record");
		}
		
		internal TableMap CreateTableMap(IMappingStrategy strategy, Type type)
		{
			return CreateTableMap(strategy, type, false, false);
		}
		
		internal TableMap CreateTableMap(IMappingStrategy strategy, Type type, bool cascadeSave, bool cascadeDelete)
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
					map = new TableMap(strategy, type, table, classAttribute, constructor, cascadeSave, cascadeDelete);
					
					IDictionary<int, DataColumn> columns = new Dictionary<int, DataColumn>();
					
					IDictionary<int, object> data = new Dictionary<int, object>();
					IList<Dictionary<int, object>> dataCollections = new List<Dictionary<int, object>>();
					if (strategy.Function == MappingFunction.Delete || strategy.Function == MappingFunction.Save)
					{
						if (strategy.Type == MappingType.Collection)
						{
							foreach(IPersistent p in strategy.Records)
								dataCollections.Add(new Dictionary<int, object>());
						}
					}
					
					int i = 1;
					foreach (PropertyInfo property in type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
					{
						foreach (PropertyAttribute attribute in property.GetCustomAttributes(typeof(PropertyAttribute), false))
						{
							if (attribute.FieldType == FieldType.Basic)
							{
								i = AddColumn(i, property, attribute, columns, data, dataCollections, strategy);
							}
							else
							{							
								PropertyMap propertyMap = new PropertyMap(property, attribute);
								TableMap childMap = null;

								if (attribute.FieldType == FieldType.ManyToOneChild)
								{
									i = AddColumn(i, property, attribute, columns, data, dataCollections, strategy);
								}
								
								childMap = GetChildMap(property, attribute, strategy);

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
