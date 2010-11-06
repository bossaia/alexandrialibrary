using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Commands;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IModel
    {
        protected RepositoryBase(IFactory<T> factory, string modelName)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (string.IsNullOrEmpty(modelName))
                throw new ArgumentNullException("modelName");

            _factory = factory;
            _modelName = modelName;
        }

        private readonly IFactory<T> _factory;
        private readonly string _modelName;

        #region Private Static Helper Methods

        private static IDbConnection GetDbConnection()
        {
            return new SQLiteConnection("Data Source=Catalog.db;Version=3;");
        }

        private static IDbCommand CreateDbCommand(IDbConnection connection, ICommand command)
        {
            var dbCommand = connection.CreateCommand();
            dbCommand.CommandType = CommandType.Text;
            dbCommand.CommandText = command.Text;

            foreach (KeyValuePair<string, object> pair in command.Parameters)
            {
                var parameter = dbCommand.CreateParameter();
                parameter.ParameterName = pair.Key;
                parameter.Value = pair.Value;
                dbCommand.Parameters.Add(parameter);
            }

            return dbCommand;
        }

        private static IDataReader GetDataReader(IDbConnection connection, ICommand command)
        {
            var dbCommand = CreateDbCommand(connection, command);
            return dbCommand.ExecuteReader();
        }

        #endregion

        #region Protected Members

        protected IFactory<T> Factory
        {
            get { return _factory; }
        }

        protected static string GerParameterName(string name)
        {
            return string.Format("@{0}", name);
        }

        protected static void ExecuteCommands(IEnumerable<ICommand> commands)
        {
            IDbTransaction transaction = null;
            
            using (var connection = GetDbConnection())
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (ICommand command in commands)
                    {
                        var dbCommand = CreateDbCommand(connection, command);
                        dbCommand.ExecuteNonQuery();
                    }

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

        protected ICollection<T> GetMany(ICommand command)
        {
            var models = new List<T>();

            using (var connection = GetDbConnection())
            {
                connection.Open();
                using (var reader = GetDataReader(connection, command))
                {
                    while (reader.Read())
                    {
                        var model = _factory.Create();
                        model.Initialize(reader["Id"]);
                        PopulateModel(model, reader);
                        models.Add(model);
                    }
                }
            }

            return models;
        }

        protected CommandBuilder GetCommandBuilderWithCallback()
        {
            return new CommandBuilder(new Action<IModel, object>((x, y) => x.Initialize(y)));
        }

        protected abstract ICommand GetInitializeCommand();
        protected abstract ICommand GetPersistCommand(T model);
        protected abstract void PopulateModel(T model, IDataRecord record);

        #endregion

        #region IRepository Members

        public void Initialize()
        {
            var commands = new List<ICommand> { GetInitializeCommand() };
            ExecuteCommands(commands);
        }

        public virtual void Persist(T model)
        {
            Persist(new List<T> { model });
        }

        public virtual void Persist(IEnumerable<T> models)
        {
            var commands = new List<ICommand>();

            foreach (var model in models)
                commands.Add(GetCommand(model));

            ExecuteCommands(commands);
        }

        public ICommand GetCommand(T model)
        {
            return GetPersistCommand(model);
        }
        
        public virtual T GetOne(object id)
        {
            var builder = GetCommandBuilderWithCallback();
            builder.AppendFormat("select * from {0} where Id =", _modelName);
            builder.AppendParameterReference("Id", id);

            var many = GetMany(builder.ToCommand());

            return many.FirstOrDefault<T>();
        }

        public ICollection<T> GetAll()
        {
            var builder = GetCommandBuilderWithCallback();
            builder.AppendFormat("select * from {0}", _modelName);

            return GetMany(builder.ToCommand());
        }

        #endregion

        /*

        #region Old Private Members

        private IDictionary<Type, ICommandBuilder> _defaultBuilders = new Dictionary<Type, ICommandBuilder>();



        private ICommandBuilder GetCommandBuilder(IIdentifiable record)
        {
            if (record.IsDeleted)
                return new DeleteTextBuilder(record);
            else if (record.IsNew)
                return new InsertTextBuilder(record);
            else if (record.IsChanged)
                return new UpdateTextBuilder(record);
            else
                return null;
        }

        private void ExecuteNonQuery(string text)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var builder = new AdHocCommandBuilder(text);
                using (var command = builder.ToCommand(connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void ExecuteNonQuery(IIdentifiable record)
        {
            IDbTransaction transaction = null;

            var builder = GetCommandBuilder(record);

            if (builder != null)
            {
                using (var connection = GetConnection())
                {
                    try
                    {

                        connection.Open();
                        transaction = connection.BeginTransaction();
                        var command = builder.ToCommand(connection, transaction);
                        command.ExecuteNonQuery();

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
        }

        private void ExecuteNonQuery(IBatch batch)
        {
            IDbTransaction transaction = null;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (var record in batch.Records)
                    {
                        var builder = GetCommandBuilder(record);
                        if (builder != null)
                        {
                            var command = builder.ToCommand(connection, transaction);
                            var value = command.ExecuteScalar();
                            batch.InvokeRecordCallback(record, value);
                        }
                    }

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



        //private static void InitializeRecord(IIdentifiable record, IDataReader reader)
        //{
        //    var data = new Dictionary<string, object>();
        //    for (var i = 0; i < reader.FieldCount; i++)
        //    {
        //        var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
        //        data.Add(reader.GetName(i), value);
        //    }
        //    record.Initialize(data);
        //}

        #endregion

        #region Old Protected Members

        protected virtual void InitializeRepository() { }
        protected abstract string Name { get; }
        protected abstract string GetInitializeCommandText();
        protected abstract IFactory<T> GetFactory<T>() where T : IIdentifiable;
        protected abstract T GetCached<T>(long id) where T : IIdentifiable;
        protected abstract ICollection<T> GetCached<T>(ICommandBuilder builder) where T : IIdentifiable;

        protected void AddBuilder<T>(ICommandBuilder builder)
        {
            _defaultBuilders[typeof(T)] = builder;
        }

        protected T GetRecord<T>(long id)
            where T : IIdentifiable
        {
            var record = default(T);
                
            var factory = GetFactory<T>();
            if (factory != null)
            {
                record = factory.Create();

                var builder = new SelectTextBuilder(record, id);
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var reader = GetDataReader(connection, builder))
                    {
                        if (reader.Read())
                        {
                            InitializeRecord(record, reader);
                        }
                    }
                }
            }

            return record;
        }

        protected ICollection<T> GetCollection<T>(ICommandBuilder builder)
            where T : IIdentifiable
        {
            IList<T> results = new List<T>();

            var factory = GetFactory<T>();
            if (factory != null)
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var reader = GetDataReader(connection, builder))
                    {
                        while (reader.Read())
                        {
                            var record = factory.Create();
                            InitializeRecord(record, reader);
                            results.Add(record);
                        }
                    }
                }
            }

            return results;
        }

        #endregion

        #region IRepository Members

        public void Initialize()
        {
            ExecuteNonQuery(GetInitializeCommandText());
            InitializeRepository();
        }

        public virtual T GetOne<T>(long id)
            where T : IModel
        {
            T record = GetCached<T>(id);
            if (record != null)
                return record;
          
            return GetModel<T>(id);
        }

        public virtual ICollection<T> GetMany<T>(ICommandBuilder builder)
            where T : IIdentifiable
        {
            ICollection<T> results = new List<T>();

            results = GetCached<T>(builder);
            if (results != null)
                return results;

            return GetCollection<T>(builder);
        }

        public virtual ICollection<T> GetAll<T>()
            where T : IModel
        {
            ICollection<T> results = new List<T>();

            if (_defaultBuilders.ContainsKey(typeof(T)))
            {
                results = Get<T>(_defaultBuilders[typeof(T)]);
            }
            else
            {
                var factory = GetFactory<T>();
                if (factory != null)
                {
                    var record = factory.Create();
                    results = GetCollection<T>(new SelectTextBuilder(record));
                }
            }

            return results;
        }

        public virtual void Persist<T>(T record)
            where T : IIdentifiable
        {
            ExecuteNonQuery(record);
        }

        public virtual void Persist(IBatch batch)
        {
            ExecuteNonQuery(batch);
        }

        #endregion

        */
    }
}
