using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CommandBuilderFactory : IFactory<ICommandBuilder>
    {
        public CommandBuilderFactory(IFactory<ICommand> factory)
        {
            _factory = factory;
        }

        private readonly IFactory<ICommand> _factory;

        public ICommandBuilder Create()
        {
            return new CommandBuilder(_factory);
        }
    }
}
