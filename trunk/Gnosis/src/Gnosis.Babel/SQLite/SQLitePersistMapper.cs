using System.Linq;
using Gnosis.Babel.SQLite.Persist.Deleting;
using Gnosis.Babel.SQLite.Persist.Inserting;
using Gnosis.Babel.SQLite.Persist.Updating;
using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite
{
    public class SQLitePersistMapper<T> : IPersistMapper<T>
        where T : IModel
    {
        public SQLitePersistMapper(ISchema<T> schema, IFactory<ICommand> commandFactory, IFactory<IDelete<T>> deleteFactory, IFactory<IInsert<T>> insertFactory, IFactory<IUpdate<T>> updateFactory, IFactory<ISelect> selectFactory)
        {
            Schema = schema;
            _commandFactory = commandFactory;
            _deleteFactory = deleteFactory;
            _insertFactory = insertFactory;
            _updateFactory = updateFactory;
            _selectFactory = selectFactory;
        }

        #region Private Members

        private readonly IFactory<ICommand> _commandFactory;
        private readonly IFactory<IDelete<T>> _deleteFactory;
        private readonly IFactory<IInsert<T>> _insertFactory;
        private readonly IFactory<IUpdate<T>> _updateFactory;
        private readonly IFactory<ISelect> _selectFactory;

        #endregion

        #region Protected Members

        protected ISchema<T> Schema { get; private set; }

        protected IDelete<T> Delete
        {
            get { return _deleteFactory.Create(); }
        }

        protected IInsert<T> Insert
        {
            get { return _insertFactory.Create(); }
        }

        protected IUpdate<T> Update
        {
            get { return _updateFactory.Create(); }
        }

        protected ISelect Select
        {
            get { return _selectFactory.Create(); }
        }

        protected virtual ICommand GetInsertCommand(T model)
        {
            var command = _commandFactory.Create();

            command.AddStatement(
                Insert
                    .OrRollback
                    .Into(Schema.Name)
                    .Columns(Schema.NonPrimaryFields.Select(x => x.Getter))
                    .Values(Schema.NonPrimaryFields.Select(x => x.Getter), model)
                );

            command.AddStatement(Select.LastInsertRowId);

            command.SetCallback((x,y) => x.Initialize(y), model);

            return command;
        }

        protected virtual ICommand GetUpdateCommand(T model)
        {
            var command = _commandFactory.Create();

            command.AddStatement(
                Update
                    .OrRollback
                    .Table(Schema.Name)
                    .Set
                    .ColumnsAndValues(Schema.NonPrimaryFields.Select(x => x.Getter), model)
                    .Where<T>(x => x.Id).IsEqualTo<T>(x => x.Id, model.Id)
                );

            return command;
        }

        protected virtual ICommand GetDeleteCommand(T model)
        {
            var command = _commandFactory.Create();

            command.AddStatement(
                Delete
                    .From(Schema.Name)
                    .Where<T>(x => x.Id).IsEqualTo<T>(x => x.Id, model.Id)
                );

            return command;
        }

        #endregion

        public ICommand GetPersistCommand(T model)
        {
            if (model.IsDeleted)
                return GetDeleteCommand(model);
            
            return (model.IsNew) ?
                GetInsertCommand(model) :
                GetUpdateCommand(model);
        }
    }
}
