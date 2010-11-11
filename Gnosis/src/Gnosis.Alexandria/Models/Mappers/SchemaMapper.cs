using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class SchemaMapper<T> : ISchemaMapper<T>
        where T : IModel
    {
        public SchemaMapper(ISchema<T> schema, IFactory<T> factory, IFactory<ICreateTableBuilder> createTableFactory, IFactory<ICreateIndexBuilder> createIndexFactory, IFactory<ICreateViewBuilder> createViewFactory)
        {
            Schema = schema;
            Factory = factory;
            CreateTableFactory = createTableFactory;
            CreateIndexFactory = createIndexFactory;
            CreateViewFactory = createViewFactory;
        }

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<T> Factory;
        protected readonly IFactory<ICreateTableBuilder> CreateTableFactory;
        protected readonly IFactory<ICreateIndexBuilder> CreateIndexFactory;
        protected readonly IFactory<ICreateViewBuilder> CreateViewFactory;

        public IEnumerable<ICommand> GetInitializeCommands()
        {
            var commands = new List<ICommand>();

            var model = Factory.Create();

            var createTable = CreateTableFactory.Create()
                .CreateTable(Schema.Name)
                .IfNotExists
                .Columns(Schema.Fields.Select(x => x.Getter), model);

            commands.Add(createTable.ToCommand());

            return commands;
        }
    }
}
