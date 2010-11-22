using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Babel.SQLite.Persist;
using Gnosis.Babel.SQLite.Persist.Deleting;
using Gnosis.Babel.SQLite.Persist.Inserting;
using Gnosis.Babel.SQLite.Persist.Updating;
using Gnosis.Babel.SQLite.Query;
using Gnosis.Babel.SQLite.Schema;

namespace Gnosis.Babel.SQLite
{
    public interface ISQLiteStatementFactory
    {
        ICreate<T> Create<T>();
        IDrop Drop();

        ISelect Select();
        
        IDelete<T> Delete<T>();
        IInsert<T> Insert<T>();
        IUpdate<T> Update<T>();
    }
}
