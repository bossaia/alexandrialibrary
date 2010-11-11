using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Repositories
{
    /*
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IModel
    {
        protected RepositoryBase(IFactory<T> factory, IModelMapper<T> modelMapper, ICommandMapper<T> commandMapper)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (modelMapper == null)
                throw new ArgumentNullException("modelMapper");
            if (commandMapper == null)
                throw new ArgumentNullException("commandMapper");

            _factory = factory;
            _modelMapper = modelMapper;
            _commandMapper = commandMapper;
        }

        private readonly IFactory<T> _factory;
        private readonly IModelMapper<T> _modelMapper;
        private readonly ICommandMapper<T> _commandMapper;

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

        protected IModelMapper<T> ModelMapper
        {
            get { return _modelMapper; }
        }

        protected ICommandMapper<T> CommandMapper
        {
            get { return _commandMapper; }
        }

        protected void Execute(IEnumerable<ICommand> commands)
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

        #endregion

        #region IRepository Members

        public void Initialize()
        {
            var commands = _commandMapper.GetInitializeCommands();
            Execute(commands);
        }

        public virtual void Persist(T model)
        {
            Persist(new List<T> { model });
        }

        public virtual void Persist(IEnumerable<T> models)
        {
            var commands = new List<ICommand>();

            foreach (var model in models)
                commands.Add(_commandMapper.GetPersistCommand(model));

            Execute(commands);
        }
        
        public virtual T GetOne(object id)
        {
            var command = _commandMapper.GetSelectOneCommand(id);

            var many = GetMany(command);

            return many.FirstOrDefault<T>();
        }

        public ICollection<T> GetMany(ICommand command)
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
                        _modelMapper.Map(model, reader);
                        models.Add(model);
                    }
                }
            }

            return models;
        }

        public ICollection<T> GetAll()
        {
            var command = _commandMapper.GetSelectAllCommand();

            return GetMany(command);
        }

        #endregion
    }
     * */
}
