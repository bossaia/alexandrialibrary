using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDropIndexBuilder : ICommandBuilder
    {
        IDropIndexBuilder DropIndex(string name);
        IDropIndexBuilder IfExists { get; }
    }
}
