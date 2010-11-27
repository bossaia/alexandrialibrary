using System.Collections.Generic;
using System.Linq;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite;
using Gnosis.Babel.SQLite.Persist.Deleting;
using Gnosis.Babel.SQLite.Persist.Inserting;
using Gnosis.Babel.SQLite.Persist.Updating;

namespace Gnosis.Alexandria.Models.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IModel
    {
        protected RepositoryBase(IStore store, ICache<T> cache, IFactory<T> factory, ISchema<T> schema, ISchemaMapper<T> schemaMapper, IModelMapper<T> modelMapper, IPersistMapper<T> persistMapper, IQueryMapper<T> queryMapper, IFactory<ICommand> commandFactory, ISQLiteStatementFactory statementFactory)
        {
            Store = store;
            Cache = cache;
            Factory = factory;
            Schema = schema;
            SchemaMapper = schemaMapper;
            ModelMapper = modelMapper;
            PersistMapper = persistMapper;
            QueryMapper = queryMapper;
            CommandFactory = commandFactory;
            StatementFactory = statementFactory;
        }

        #region Protected Members

        protected readonly IStore Store;
        protected readonly ICache<T> Cache;
        protected readonly IFactory<T> Factory;
        protected readonly ISchema<T> Schema;
        protected readonly ISchemaMapper<T> SchemaMapper;
        protected readonly IModelMapper<T> ModelMapper;
        protected readonly IPersistMapper<T> PersistMapper;
        protected readonly IQueryMapper<T> QueryMapper;
        protected readonly IFactory<ICommand> CommandFactory;
        protected readonly ISQLiteStatementFactory StatementFactory;

        protected IInsert<T> Insert { get { return StatementFactory.Insert<T>(); } }
        protected IUpdate<T> Update { get { return StatementFactory.Update<T>(); } }
        protected IDelete<T> Delete { get { return StatementFactory.Delete<T>(); } }

        #endregion

        #region IRepository Members

        public virtual void Initialize()
        {
            var commands = SchemaMapper.GetInitializeCommands();
            Store.Execute(commands);
        }

        public virtual void Persist(T model)
        {
            Persist(new List<T> { model });
        }

        public virtual void Persist(IEnumerable<T> models)
        {
            var commands = new List<ICommand>();

            models.Each(x => commands.Add(PersistMapper.GetPersistCommand(x)));

            Store.Execute(commands);
        }
        
        public virtual T GetOne(object id)
        {
            var command = QueryMapper.GetSelectOneCommand(id);

            var many = GetMany(command);

            return many.FirstOrDefault();
        }

        public virtual ICollection<T> GetMany(ICommand command)
        {
            return Store.Query(command, ModelMapper, Cache);
        }

        public virtual ICollection<T> GetAll()
        {
            var command = QueryMapper.GetSelectAllCommand();

            return GetMany(command);
        }

        #endregion
    }
}
