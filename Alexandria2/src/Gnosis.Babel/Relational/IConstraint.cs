using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IConstraint
        : INamed
    {
        ITable Table { get; }
        string GetCreateSql();
        string GetDropSql();
    }
}
