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

            var createTable = CreateStatementFactory.Create();
            var x = createTable.TableIfNotExists(Schema.Name);
            var y = x.Column(Schema.PrimaryField.Getter);
            var z = y.Integer;
            var a = z.NotNull;

            //foreach (var field in Schema.NonPrimaryFields)
                //createTable = createTable.Column(field.Getter, field.Getter.GetValue(model).AsAffinity()).NotNull.Default(field.Getter.GetValue(model));

            command.AddStatement(createTable);

            commands.Add(command);

            return commands;
        }
    }
}
