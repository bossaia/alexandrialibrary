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
using System.Data;
using System.Linq;
using System.Text;

namespace Telesophy.Babel.Persistence
{
	public abstract class EngineBase<ConnectionType, TransactionType, CommandType>
		: IEngine
		where ConnectionType: IDbConnection
		where TransactionType : IDbTransaction
		where CommandType : IDbCommand
	{
		#region Constructors
		protected EngineBase(string name)
		{
			this.name = name;
		}
		#endregion
		
		#region Private Fields
		private string name;
		private IDataConverter dataConverter;
		#endregion
	
		#region Protected Properties
		protected abstract string DatabaseDirectory { get; }
		#endregion
	
		#region Protected Methods
		protected abstract string GetConnectionString(Schema schema);
		
		protected virtual ConnectionType GetConnection(Schema schema)
		{
			if (schema != null)
			{
				return GetConnection(GetConnectionString(schema));
			}
			
			return default(ConnectionType);
		}
		
		protected abstract ConnectionType GetConnection(string connectionString);
		
		protected abstract TransactionType GetTransaction(ConnectionType connection);
		
		protected abstract CommandType GetCommand(ConnectionType connection, TransactionType transaction, string commandText);
		
		protected abstract CommandType GetSelectCommand(ConnectionType connection, TransactionType transaction, Entity entity, IExpression filter);

		protected virtual CommandType GetSelectCommand(ConnectionType connection, TransactionType transaction, Map map, IExpression filter)
		{
			StringBuilder sql = new StringBuilder("SELECT ");

			sql.Append(map.Leaf.GetFieldList(map));
			
			sql.AppendFormat(" FROM {0}" + map.Leaf.Name);
			
			const string JOIN_FORMAT = " {0} JOIN {1} ON {2}.{3} = {1}.{4}";
			
			foreach (Association branch in map.Branches)
			{
				string joinType = (branch.IsRequired) ? "INNER" : "LEFT OUTER";
			
				// LEFT INNER JOIN MediaSetItems ON MediaSet.Id = MediaSetItems.ParentId
				// LEFT INNER JOIN MediaItem ON MediaSetItems.ChildId = MediaItem.Id
				
				sql.AppendFormat(JOIN_FORMAT, joinType, branch.Name, branch.Parent.Name, branch.Parent.Identifier.Name, branch.ParentFieldName);
				sql.AppendFormat(JOIN_FORMAT, joinType, branch.Child.Name, branch.Name, branch.ChildFieldName, branch.Child.Identifier.Name);
			}
			
			sql.AppendFormat(GetWhereClause(filter));
			
			return GetCommand(connection, transaction, sql.ToString());
		}
		
		protected abstract void CreateEntityTables(Entity entity, ConnectionType connection, TransactionType transaction);
		
		protected abstract string GetWhereClause(IExpression filter);
		#endregion
	
		#region IEngine Members
		public string Name
		{
			get { return name; }
		}

		public IDataConverter DataConverter
		{
			get { return dataConverter; }
			set { dataConverter = value; }
		}

		public virtual void Initialize(Schema schema)
		{
			if (schema != null)
			{
				using (ConnectionType connection = GetConnection(schema))
				{
					connection.Open();
					TransactionType transaction = default(TransactionType);

					try
					{
						transaction = GetTransaction(connection);

						foreach (Entity entity in schema.Entities)
						{
							CreateEntityTables(entity, connection, transaction);
						}

						transaction.Commit();
					}
					catch (Exception ex)
					{
						if (transaction != null)
							transaction.Rollback();

						throw ex;
					}
				}
			}
		}

		public virtual IList<T> Load<T>(Aggregate<T> aggregate, IExpression filter)
		{
			IList<T> list = new List<T>();
			
			if (aggregate != null)
			{
				using (ConnectionType connection = GetConnection(aggregate.Schema))
				{
					TransactionType transaction = default(TransactionType);
				
					try
					{
						transaction = GetTransaction(connection);
						
						DataSet dataSet = new DataSet(aggregate.Name);
						
						DataTable rootTable = aggregate.Root.GetDataTable(aggregate.Name);
						
						dataSet.Tables.Add(rootTable);
						CommandType rootSelect = GetSelectCommand(connection, transaction, aggregate.Root, filter);
						IDataReader rootReader = rootSelect.ExecuteReader();
						while (rootReader.Read())
						{
							aggregate.Root.AddDataRow(rootTable, rootReader, DataConverter, null);
						}
						
						foreach (Map map in aggregate.Maps)
						{
							DataTable table = null;
							if (!dataSet.Tables.Contains(map.Name))
							{
								table = map.Leaf.GetDataTable(map);
								dataSet.Tables.Add(table);
							}
							else table = dataSet.Tables[map.Name];
							
							CommandType entitySelect = GetSelectCommand(connection, transaction, map, filter);
							IDataReader entityReader = entitySelect.ExecuteReader();
							while (entityReader.Read())
							{
								map.Leaf.AddDataRow(table, entityReader, DataConverter, map);
							}							
						}
						
						transaction.Commit();
						
						list = aggregate.Load(dataSet);
					}
					catch (Exception ex)
					{
						if (transaction != null)
							transaction.Rollback();
							
						throw ex;
					}
				}
			}
			
			return list;
		}

		public abstract void Save<T>(Aggregate<T> aggregate, IEnumerable<T> models);

		public abstract void Delete<T>(Aggregate<T> aggregate, IEnumerable<T> models);

		public abstract Type GetTypeForEngine<EntityType>();

		public abstract object GetValueForEngine<EntityValue>(EntityValue entityValue);

		public abstract EntityValue GetValueForEntity<EntityValue>(object engineValue);
		#endregion
	}
}
