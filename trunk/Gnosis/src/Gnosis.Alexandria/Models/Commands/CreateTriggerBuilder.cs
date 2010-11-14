using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CreateTriggerBuilder : CommandBuilder, ICreateTriggerBuilder
    {
        public CreateTriggerBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
        }

        public ICreateTriggerBuilder CreateTempTrigger(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateTriggerBuilder CreateTrigger(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateTriggerBuilder IfNotExists
        {
            get { throw new NotImplementedException(); }
        }

        public ICreateTriggerBuilder BeforeInsertOn(string table)
        {
            throw new NotImplementedException();
        }

        public ICreateTriggerBuilder BeforeUpdateOn(string table)
        {
            throw new NotImplementedException();
        }

        public ICreateTriggerBuilder UpdatedColumn(string column)
        {
            throw new NotImplementedException();
        }

        public ICreateTriggerBuilder BeforeDeleteOn(string table)
        {
            throw new NotImplementedException();
        }

        public ICreateTriggerBuilder When()
        {
            throw new NotImplementedException();
        }
    }
}
