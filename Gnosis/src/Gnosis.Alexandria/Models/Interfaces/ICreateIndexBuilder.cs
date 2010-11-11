using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface ICreateIndexBuilder : ICommandBuilder
    {
        ICreateIndexBuilder CreateUniqueIndex(string name);
        ICreateIndexBuilder CreateIndex(string name);

        ICreateIndexBuilder IfNotExists { get; }

        ICreateIndexBuilder On(string table);
        ICreateIndexBuilder Ascending(string column);
        ICreateIndexBuilder Descending(string column);
    }
}
