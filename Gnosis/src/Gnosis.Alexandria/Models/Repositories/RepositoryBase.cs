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
        protected RepositoryBase(IFactory<T> factory, IModelMapper<T> mapper, string modelName)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            if (mapper == null)
                throw new ArgumentNullException("mapper");
            if (string.IsNullOrEmpty(modelName))
                throw new ArgumentNullException("modelName");

            _factory = factory;
            _mapper = mapper;
            _modelName = modelName;
        }

        private readonly IFactory<T> _factory;
        private readonly IModelMapper<T> _mapper;
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

        protected IModelMapper<T> Mapper
        {
            get { return _mapper; }
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
                        _mapper.Map(model, reader);
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
    }
}
