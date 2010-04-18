using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IColumnExpression
    {
        IColumn Column { get; }
        string Alias { get; }
    }
}
