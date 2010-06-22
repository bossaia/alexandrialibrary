using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Persistence
{
    public interface ITable
    {
        string Name { get; }
        IColumn PrimaryKey { get; }
        IEnumerable<IColumn> Columns { get; }
        ITable AddColumn(string name, Type type);
        ITable AddColumn(string name, Type type, object @default);
    }
}
