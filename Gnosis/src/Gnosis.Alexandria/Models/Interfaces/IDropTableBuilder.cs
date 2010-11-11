using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDropTableBuilder : ICommandBuilder
    {
        IDropTableBuilder DropTable(string name);
        IDropTableBuilder IfExists { get; }
    }
}
