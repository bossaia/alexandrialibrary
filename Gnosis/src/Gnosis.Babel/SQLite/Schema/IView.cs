using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.SQLite.Query;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface IView : IStatement
    {
        ISelect AsSelect { get; }
    }

    public interface IView<T> : IStatement
    {
        ISelect AsSelect { get; }
    }
}
