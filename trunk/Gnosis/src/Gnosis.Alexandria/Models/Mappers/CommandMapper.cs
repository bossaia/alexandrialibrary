using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Mappers
{
    /*
    public abstract class CommandMapper<T> : ICommandMapper<T>
        where T : IModel
    {
        protected CommandMapper(string modelName,
            IFactory<IDeleteBuilder> deleteBuilderFactory,
            IFactory<ICreateTableBuilder>
            )
        {
            _factory = factory;
            _modelName = modelName;
        }

        private readonly IFactory<IInsertBuilder> _insertBuilderFactory;
        private readonly IFactory<IUpdateBuilder> _updateBuilderFactory;
        private readonly IFactory<IDeleteBuilder> _deleteBuilderFactory;
        private readonly string _modelName;

        protected abstract IEnumerable<ICommand> GetInitializeCommandsInternal();
        protected abstract IDictionary<string, object> GetPersistenceMap(T model);

        protected virtual ICommand GetSaveCommand(ICommandBuilder builder, T model)
        {
            var map = GetPersistenceMap(model);
            
            //TODO: Change ICommandBuilder to make this less awkward
            builder.AppendFormat("insert or replace into {0} (", _modelName);

            var delimiter = string.Empty;
            foreach (var column in map.Keys)
            {
                builder.AppendFormat("{0}{1}", delimiter, column);
                delimiter = ",";
            }

            builder.Append(") values (");

            delimiter = string.Empty;
            foreach (var item in map)
            {
                builder.Append(delimiter);
                builder.AppendParameter(item.Key, item.Value);
                delimiter = ",";
            }

            builder.Append(")");
            return builder.ToCommand();
        }

        protected virtual ICommand GetDeleteCommand(IDeleteBuilder builder, object id)
        {
            return builder
                .DeleteFrom(_modelName)
                .Where("Id")
                .IsEqualTo("@Id", id)
                .ToCommand();
        }

        public ICommandBuilder GetCommandBuilder()
        {
            return _factory.Create();
        }

        public IEnumerable<ICommand> GetInitializeCommands()
        {
            return GetInitializeCommandsInternal();
        }

        public ICommand GetPersistCommand(T model)
        {
            var builder = _factory.Create();

            if (model.IsDeleted)
                return GetDeleteCommand(builder, model.Id);
            else
                return GetSaveCommand(builder, model);
        }

        public ICommand GetSelectOneCommand(object id)
        {
            var builder = _factory.Create();
            builder.AppendFormat("select * from {0} where Id =", _modelName);
            builder.AppendParameter("Id", id);

            return builder.ToCommand();
        }

        public ICommand GetSelectAllCommand()
        {
            var builder = _factory.Create();
            builder.AppendFormat("select * from {0}", _modelName);
            
            return builder.ToCommand();
        }
        
    }
    */
}
