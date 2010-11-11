using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDropViewBuilder : ICommandBuilder
    {
        IDropViewBuilder DropView(string name);
        IDropViewBuilder IfExists { get; }
    }
}
