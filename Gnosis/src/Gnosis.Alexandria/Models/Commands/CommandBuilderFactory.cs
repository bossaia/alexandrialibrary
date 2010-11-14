using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CommandBuilderFactory : 
        IFactory<IInsertBuilder>,
        IFactory<IUpdateBuilder>,
        IFactory<IDeleteBuilder>,
        IFactory<ISelectBuilder>,
        IFactory<ICreateTableBuilder>,
        IFactory<ICreateIndexBuilder>,
        IFactory<ICreateViewBuilder>,
        IFactory<ICreateTriggerBuilder>
    {
        public CommandBuilderFactory(IFactory<ICommand> factory)
        {
            _factory = factory;
        }

        private readonly IFactory<ICommand> _factory;

        IInsertBuilder IFactory<IInsertBuilder>.Create()
        {
            return new InsertBuilder(_factory);
        }

        IUpdateBuilder IFactory<IUpdateBuilder>.Create()
        {
            return new UpdateBuilder(_factory);
        }

        IDeleteBuilder IFactory<IDeleteBuilder>.Create()
        {
            return new DeleteBuilder(_factory);
        }

        ISelectBuilder IFactory<ISelectBuilder>.Create()
        {
            return new SelectBuilder(_factory);
        }

        ICreateTableBuilder IFactory<ICreateTableBuilder>.Create()
        {
            return new CreateTableBuilder(_factory);
        }

        ICreateIndexBuilder IFactory<ICreateIndexBuilder>.Create()
        {
            return new CreateIndexBuilder(_factory);
        }

        ICreateViewBuilder IFactory<ICreateViewBuilder>.Create()
        {
            return new CreateViewBuilder(_factory);
        }

        ICreateTriggerBuilder IFactory<ICreateTriggerBuilder>.Create()
        {
            return new CreateTriggerBuilder(_factory);
        }
    }
}
