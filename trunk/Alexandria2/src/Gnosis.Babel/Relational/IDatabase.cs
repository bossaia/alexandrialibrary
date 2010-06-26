using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IDatabase
        : INamed
    {
        IHost Host { get; }
        string Path { get; }
        IEnumerable<ITable> GetTables();

        ITable CreateTable(string name);
        ITable CreateTable(string name, bool isTemporary);

        void AddTable(ITable table);
    }
}
