using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Gnosis.Babel.SQLite
{
    public abstract class SQLiteStore : IStore
    {
        protected SQLiteStore(string name)
        {
            _name = name;
        }

        private readonly string _name;

        private string GetConnectionString()
        {
            return string.Format("Data Source={0}.db;Version=3;", _name);
        }

        private IDbConnection GetDbConnection()
        {
            return GetDbConnection(GetConnectionString());
        }

        private static IDbConnection GetDbConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        private static IDbCommand CreateDbCommand(ICommand command, IDbConnection connection, IDbTransaction transaction = null)
        {
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = command.ToString();

            if (transaction != null)
                dbCommand.Transaction = transaction;

            foreach (var pair in command.Parameters)
            {
                var parameter = dbCommand.CreateParameter();
                parameter.ParameterName = pair.Key;
                parameter.Value = pair.Value;
                dbCommand.Parameters.Add(parameter);
            }

            return dbCommand;
        }

        private static IDataReader GetDataReader(ICommand command, IDbConnection connection)
        {
            var dbCommand = CreateDbCommand(command, connection);
            return dbCommand.ExecuteReader();
        }

        private static void Execute(ICommand command, IDbConnection connection, IDbTransaction transaction)
        {
            var dbCommand = CreateDbCommand(command, connection, transaction);
            var result = dbCommand.ExecuteScalar();

            command.InvokeCallback(result);
        }

        public string Name
        {
            get { return _name; }
        }

        public void Execute(IEnumerable<ICommand> commands)
        {
            IDbTransaction transaction = null;

            using (var connection = GetDbConnection())
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (var command in commands)
                        Execute(command, connection, transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction != null)
                        transaction.Rollback();

                    throw;
                }
            }
        }

        public ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper)
        {
            return Query(command, mapper, null);
        }

        public ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper, ICache<T> cache)
        {
            var results = new List<T>();

            using (var connection = GetDbConnection())
            {
                connection.Open();
                using (var reader = GetDataReader(command, connection))
                {
                    while (reader.Read())
                    {
                        var model = default(T);

                        if (cache != null && !cache.IsEmpty)
                        {
                            var id = mapper.GetId(reader);
                            model = cache.GetOne(id);
                        }

                        if (model == null)
                            model = mapper.GetModel(reader);
                        
                        results.Add(model);
                    }
                }
            }

            return results;
        }
    }
}
