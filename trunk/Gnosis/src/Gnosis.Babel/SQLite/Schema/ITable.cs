using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITable : IStatement, IColumnar
    {
        ISelect AsSelect { get; }
    }

    public interface ITable<T> : IStatement, IColumnar<T>
    {
        ISelect AsSelect { get; }
    }
}
