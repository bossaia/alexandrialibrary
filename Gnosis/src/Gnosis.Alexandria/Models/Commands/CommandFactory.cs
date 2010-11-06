using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Commands
{
    public class CommandFactory : IFactory<ICommand>
    {
        public ICommand Create()
        {
            return new Command();
        }
    }
}
