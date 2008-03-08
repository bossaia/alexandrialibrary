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
		
		protected abstract CommandType GetCommand(string commandText, ConnectionType connection, TransactionType transaction);
		
		protected abstract void CreateEntityTables(Entity entity, ConnectionType connection, TransactionType transaction);
		#endregion
	
		#region IEngine Members
		public string Name
		{
			get { return name; }
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
				DataSet dataSet = aggregate.GetDataSet();
			
				Dictionary<string, CommandType> commands = new Dictionary<string,CommandType>();
				//commands.Add
			 
				list = aggregate.Load(dataSet);
			}
			
			return list;
		}

		public abstract void Save<T>(Aggregate<T> aggregate, IEnumerable<T> models);

		public abstract void Delete<T>(Aggregate<T> aggregate, IEnumerable<T> models);

		public abstract object GetValueForEngine<EntityValue>(EntityValue entityValue);

		public abstract EntityValue GetValueForEntity<EntityValue>(object engineValue);
		#endregion
	}
}
