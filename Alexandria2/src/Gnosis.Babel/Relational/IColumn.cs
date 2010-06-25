using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IColumn
        : INamed
    {
        ITable Table { get; }
        string DataType { get; }
        Size Size { get; }
        bool IsRequired { get; }
    }
}
