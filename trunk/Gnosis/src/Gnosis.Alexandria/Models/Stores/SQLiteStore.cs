using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Stores
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

        private static IDbCommand CreateDbCommand(ICommand command, IDbConnection connection)
        {
            return CreateDbCommand(command, connection, null);
        }

        private static IDbCommand CreateDbCommand(ICommand command, IDbConnection connection, IDbTransaction transaction)
        {
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = command.Text;

            if (transaction != null)
                dbCommand.Transaction = transaction;

            foreach (KeyValuePair<string, object> pair in command.Parameters)
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
            object result = dbCommand.ExecuteScalar();

            if (command.Callback != null && command.Model != null)
                command.Callback(command.Model, result);
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

                    commands.Each(x => Execute(x, connection, transaction));

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
            where T : IModel
        {
            return Query<T>(command, mapper, null);
        }

        public ICollection<T> Query<T>(ICommand command, IModelMapper<T> mapper, ICache<T> cache)
            where T : IModel
        {
            var results = new List<T>();

            using (var connection = GetDbConnection())
            {
                connection.Open();
                using (var reader = GetDataReader(command, connection))
                {
                    while (reader.Read())
                    {
                        T model = default(T);

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
