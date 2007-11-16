using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Telesophy.Alexandria.Persistence.Common
{
	public abstract class SqlDataMap<T> : IDataMap<T>
	{
		public SqlDataMap()
		{
		}
		
		#region Protected Members
		protected abstract string RecordName { get; }
		
		protected abstract string IdName { get; }
		
		protected abstract IDbConnection GetConnection();
		
		protected abstract IDbCommand GetCommand();
		
		protected virtual string GetSelectSql(string id)
		{
			return string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", RecordName, IdName, id);
		}
		
		protected virtual IDataReader GetLookupReader(IDbConnection connection, Uri id)
		{
			IDbCommand command = GetCommand();
			command.Connection = connection;
			command.CommandType = CommandType.Text;
			command.CommandText = GetSelectSql(id.ToString());
			return command.ExecuteReader();
		}
		
		protected abstract T GetRecord(IDataReader reader);
		#endregion
	
		#region IDataMap<T> Members
		public virtual T Lookup(Uri id)
		{
			using (IDbConnection connection = GetConnection())
			{
				IDataReader reader = GetLookupReader(connection, id);
				if (reader != null)
					return GetRecord(reader);
				else return default(T);
			}
		}

		public void Save(T record)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Delete(T record)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion
	}
}
