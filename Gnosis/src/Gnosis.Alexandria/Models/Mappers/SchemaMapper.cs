using System.Collections.Generic;
using System.Linq;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Babel;
using Gnosis.Babel.SQLite.Schema;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class SchemaMapper<T> : ISchemaMapper<T>
        where T : IModel
    {
        public SchemaMapper(ISchema<T> schema, IFactory<T> factory, IFactory<ICreate<T>> createStatementFactory)
        {
            Schema = schema;
            Factory = factory;
            CreateStatementFactory = createStatementFactory;
        }

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<T> Factory;
        protected readonly IFactory<ICreate<T>> CreateStatementFactory;

        public IEnumerable<ICommand> GetInitializeCommands()
        {
            var commands = new List<ICommand>();

            var model = Factory.Create();

            var createTable = CreateStatementFactory.Create()
                .TableIfNotExists(Schema.Name)
                .Column(Schema.PrimaryField.Getter).Integer.NotNull;

            foreach (var field in Schema.NonPrimaryFields)
            {
                var getter = field.Getter.Compile();
                createTable = createTable.Column(field.Getter).Text.NotNull.Default(getter(model));
            }

            /*
            var createTable = CreateTableFactory.Create()
                .CreateTable
                .IfNotExists
                .Name(Schema.Name)
                .PrimaryKey<T>(Schema.PrimaryField.Getter, model)
                .Columns<T>(Schema.NonPrimaryFields.Select(x => x.Getter), model);
            */
            //commands.Add(createTable.ToCommand());))

            return commands;
        }
    }
}
