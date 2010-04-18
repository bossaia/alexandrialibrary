using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IAlterTable
    {
        ITable Table { get; }
        string Rename { get; }
        IColumn Add { get; }
    }
}
