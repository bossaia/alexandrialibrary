using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CreateIndexBuilder : CommandBuilder, ICreateIndexBuilder
    {
        public CreateIndexBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
        }

        public ICreateIndexBuilder CreateUniqueIndex(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateIndexBuilder CreateIndex(string name)
        {
            throw new NotImplementedException();
        }

        public ICreateIndexBuilder IfNotExists
        {
            get { throw new NotImplementedException(); }
        }

        public ICreateIndexBuilder On(string table)
        {
            throw new NotImplementedException();
        }

        public ICreateIndexBuilder Ascending(string column)
        {
            throw new NotImplementedException();
        }

        public ICreateIndexBuilder Descending(string column)
        {
            throw new NotImplementedException();
        }
    }
}
