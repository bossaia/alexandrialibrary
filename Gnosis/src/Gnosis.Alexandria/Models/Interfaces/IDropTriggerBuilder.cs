using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDropTriggerBuilder : ICommandBuilder
    {
        IDropTriggerBuilder DropTrigger(string name);
        IDropTriggerBuilder IfExists { get; }
    }
}
