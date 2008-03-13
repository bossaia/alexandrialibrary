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
	public abstract class EngineBase
		<ConnectionType, TransactionType, CommandType, ParameterType>
		: IEngine
		where ConnectionType: IDbConnection
		where TransactionType : IDbTransaction
		where CommandType : IDbCommand
		where ParameterType : IDataParameter
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
		protected abstract string GetConnectionString(ISchema schema);
		
		protected virtual ConnectionType GetConnection(ISchema schema)
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

		protected abstract CommandType GetCommand(ConnectionType connection, TransactionType transaction, string commandText, IList<ParameterType> parameters);
		
		protected virtual CommandType GetSelectCommand(ConnectionType connection, TransactionType transaction, Entity entity, IQuery query)
		{
			StringBuilder sql = new StringBuilder("SELECT ");

			sql.Append(entity.GetFieldList());
			
			sql.AppendFormat(" FROM {0}" + entity.Name);

			IList<ParameterType> parameters;
			sql.Append(GetWhereClause(query, out parameters));

			return GetCommand(connection, transaction, sql.ToString(), parameters);
		}

		protected virtual CommandType GetSelectCommand(ConnectionType connection, TransactionType transaction, Map map, IQuery query)
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
			
			IList<ParameterType> parameters;
			sql.Append(GetWhereClause(query, out parameters));
			
			return GetCommand(connection, transaction, sql.ToString(), parameters);
		}
		
		protected abstract ParameterType GetParameter(string name, object value);
		
		protected abstract void CreateEntityTables(Entity entity, ConnectionType connection, TransactionType transaction);
				
		protected virtual string GetWhereClause(IQuery query, out IList<ParameterType> parameters)
		{
			parameters = new List<ParameterType>();
		
			if (query != null && query.Filters.Count > 0)
			{
				StringBuilder clause = new StringBuilder(" WHERE");

				int i = 0;
				foreach (IExpression filter in query.Filters)
				{
					i++;
					string name = string.Format("@{0}", i);

					if (filter.LinkingOperator != null)
					{
						clause.AppendFormat(" {0}", filter.LinkingOperator);
					}

					clause.AppendFormat(" {0} {1} {2}", filter.LeftOperand, filter.ComparisonOperator, name);

					object value = filter.RightOperand;
					if (filter.ComparisonOperator.Name.Equals("LIKE", StringComparison.InvariantCultureIgnoreCase))
						value = string.Format("%{0}%", filter.RightOperand);
					
					parameters.Add(GetParameter(name, value));
				}
			}
			
			return string.Empty;
		}
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

		public virtual void Initialize(ISchema schema)
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

		public virtual IList<T> Load<T>(Aggregate<T> aggregate, IQuery query)
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
						CommandType rootSelect = GetSelectCommand(connection, transaction, aggregate.Root, query);
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
							
							CommandType entitySelect = GetSelectCommand(connection, transaction, map, query);
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

		public virtual void Save<T>(Aggregate<T> aggregate, IEnumerable<T> models)
		{
			if (aggregate != null)
			{
				using (ConnectionType connection = GetConnection(aggregate.Schema))
				{
					TransactionType transaction = default(TransactionType);
				
					try
					{
						transaction = GetTransaction(connection);
						DateTime timeStamp = DateTime.Now;
						
						DataSet dataSet = aggregate.GetDataSet(models, timeStamp);
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

		public abstract void Delete<T>(Aggregate<T> aggregate, IEnumerable<T> models);
		#endregion
	}
}
