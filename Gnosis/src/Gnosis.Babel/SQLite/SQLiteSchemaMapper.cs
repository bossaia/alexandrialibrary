using System.Collections.Generic;

using Gnosis.Babel.SQLite.Schema;

namespace Gnosis.Babel.SQLite
{
    public class SQLiteSchemaMapper<T> : ISchemaMapper<T>
        where T : IModel
    {
        public SQLiteSchemaMapper(ISchema<T> schema, IFactory<T> modelFactory, IFactory<ICommand> commandFactory, IFactory<ICreate<T>> createStatementFactory)
        {
            Schema = schema;
            ModelFactory = modelFactory;
            CommandFactory = commandFactory;
            CreateStatementFactory = createStatementFactory;
        }

        protected readonly ISchema<T> Schema;
        protected readonly IFactory<T> ModelFactory;
        protected readonly IFactory<ICommand> CommandFactory;
        protected readonly IFactory<ICreate<T>> CreateStatementFactory;

        public IEnumerable<ICommand> GetInitializeCommands()
        {
            var commands = new List<ICommand>();

            var model = ModelFactory.Create();

            var command = CommandFactory.Create();

            var createTable = CreateStatementFactory.Create()
                .TableIfNotExists(Schema.Name)
                .Column(Schema.PrimaryField.Getter).Integer.NotNull;

            foreach (var field in Schema.NonPrimaryFields)
            {
                var getter = field.Getter.Compile();
                var defaultValue = getter(model);
                createTable = createTable.Column(field.Getter, defaultValue.AsAffinity()).NotNull.Default(defaultValue);
            }

            command.AddStatement(createTable);

            commands.Add(command);

            return commands;
        }
    }
}
