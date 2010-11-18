using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.SQLite.Query
{
    public interface ISourcable
    {
        IFrom From(string table);
        IFrom From(string table, string alias);
    }
}
