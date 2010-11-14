using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CreateViewBuilder : CommandBuilder, ICreateViewBuilder
    {
        public CreateViewBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
        }

        public ICreateViewBuilder CreateView(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateViewBuilder CreateTempView(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateViewBuilder IfNotExists
        {
            get { throw new NotImplementedException(); }
        }

        public ICreateViewBuilder As(ICommand select)
        {
            throw new NotImplementedException();
        }

        public ICreateViewBuilder AddParameter(string name, object value)
        {
            throw new NotImplementedException();
        }
    }
}
