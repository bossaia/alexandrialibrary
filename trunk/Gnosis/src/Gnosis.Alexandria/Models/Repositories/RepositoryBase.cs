using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Utilities;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Repositories
{
    public abstract class RepositoryBase<T>
        : IRepository<T>
        where T : IModel
    {
        protected RepositoryBase(IStore store, IFactory<T> factory, ISchema<T> schema, ISchemaMapper<T> schemaMapper, IModelMapper<T> modelMapper, IPersistMapper<T> persistMapper, IQueryMapper<T> queryMapper, IFactory<ISelectBuilder> selectFactory)
        {
            Store = store;
            Factory = factory;
            Schema = schema;
            SchemaMapper = schemaMapper;
            ModelMapper = modelMapper;
            PersistMapper = persistMapper;
            QueryMapper = queryMapper;
            SelectFactory = selectFactory;
        }

        #region Protected Members

        protected readonly IStore Store;
        protected readonly IFactory<T> Factory;
        protected readonly ISchema<T> Schema;
        protected readonly ISchemaMapper<T> SchemaMapper;
        protected readonly IModelMapper<T> ModelMapper;
        protected readonly IPersistMapper<T> PersistMapper;
        protected readonly IQueryMapper<T> QueryMapper;
        protected readonly IFactory<ISelectBuilder> SelectFactory;

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

            return many.FirstOrDefault<T>();
        }

        public virtual ICollection<T> GetMany(ICommand command)
        {
            return Store.Query(command, ModelMapper);
        }

        public virtual ICollection<T> GetAll()
        {
            var command = QueryMapper.GetSelectAllCommand();

            return GetMany(command);
        }

        #endregion
    }
}
