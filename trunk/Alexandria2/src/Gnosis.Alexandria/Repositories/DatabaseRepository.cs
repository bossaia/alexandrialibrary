using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;

using Gnosis.Alexandria.Factories;
using Gnosis.Alexandria.Mapping;

namespace Gnosis.Alexandria.Repositories
{
	public abstract class DatabaseRepository<T>
		: RepositoryBase, IRepository<T>
		where T : IEntity
	{
		public DatabaseRepository(IContext context, IFactory<T> factory, IClassMap<T> map)
			: base(context)
		{
			_factory = factory;
			_map = map;
		}

		private readonly IFactory<T> _factory;
		private readonly IClassMap<T> _map;

		private string GetConnectionString()
		{
			return "Data Source=Catalog.db;UTF8Encoding=True;Version=3";
		}

		private SQLiteConnection GetConnection()
		{
			return new SQLiteConnection(GetConnectionString());
		}

		protected IClassMap<T> Map
		{
			get { return _map; }
		}

		protected IList<T> List(string commandText)
		{
			IList<T> list = new List<T>();

			using (var connection = GetConnection())
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = commandText;

				using (var reader = command.ExecuteReader())
				{
					list = Map.Load(reader);
				}
			}

			return list;
		}

		protected void ExecuteNonQuery(string commandText)
		{
			SQLiteTransaction transaction = null;

			try
			{
				using (var connection = GetConnection())
				{
					connection.Open();
					transaction = connection.BeginTransaction();

					using (var command = connection.CreateCommand())
					{
						command.CommandText = commandText;
						command.ExecuteNonQuery();
					}

					transaction.Commit();
				}
			}
			catch (Exception)
			{
				if (transaction != null)
					transaction.Rollback();

				throw;
			}
		}

		#region IRepository<T> Members

		public T Create()
		{
			return _factory.Create();
		}

		public T Get(long id)
		{
			string commandText = string.Format("SELECT * FROM {0} WHERE {1} = {2}" + Map.Table, Map.Key, Map.GetValue(id));
			IList<T> list = List(commandText);

			if (list != null && list.Count == 1)
				return list[0];

			return default(T);
		}

		public IList<T> GetAll()
		{
			string commandText = string.Format("SELECT * FROM {0}", Map.Table);

			return List(commandText);
		}

		public void Initialize()
		{
			ExecuteNonQuery(Map.GetInitializeCommandText());
		}

		public void Save(T entity)
		{
			ExecuteNonQuery(Map.GetSaveCommandText(entity));
		}

		public void Delete(long id)
		{
			ExecuteNonQuery(Map.GetDeleteCommandText(id));
		}

		#endregion
	}
}
