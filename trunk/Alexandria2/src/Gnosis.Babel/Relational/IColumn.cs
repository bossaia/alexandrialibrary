using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IColumn
    {
        ITable Table { get; }
        string Name { get; }
        Type DataType { get; }
        object Default { get; }

        void SetTable(ITable table);
    }
}
