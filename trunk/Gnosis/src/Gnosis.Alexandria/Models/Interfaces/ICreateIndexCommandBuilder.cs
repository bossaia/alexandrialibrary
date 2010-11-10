using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateIndexCommandBuilder
    {
        ICreateIndexCommandBuilder CreateUniqueIndex(string name);
        ICreateIndexCommandBuilder CreateIndex(string name);

        ICreateIndexCommandBuilder IfNotExists { get; }

        ICreateIndexCommandBuilder On(string table);
        ICreateIndexCommandBuilder Ascending(string column);
        ICreateIndexCommandBuilder Descending(string column);

        ICommand ToCommand();
    }
}
