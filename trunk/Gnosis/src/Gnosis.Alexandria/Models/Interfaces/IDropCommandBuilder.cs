using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IDropCommandBuilder
    {
        IDropCommandBuilder DropIndex(string name);
        IDropCommandBuilder DropTable(string name);
        IDropCommandBuilder DropTrigger(string name);
        IDropCommandBuilder DropView(string name);
        IDropCommandBuilder IfExists { get; }
    }
}
