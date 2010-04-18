using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface IDialect
    {
        string Name { get; }
        string Version { get; }

        IDbConnection Connection(string connectionString);
        IDbTransaction Transaction();

        IDbCommand Alter(IAlterTable alterTable);

        IDbCommand Create(IDatabase database);
        IDbCommand Create(IIndex index);
        IDbCommand Create(ITable table);
        IDbCommand Create(ITrigger trigger);
        IDbCommand Create(IView view);

        IDbCommand Drop(IDatabase database);
        IDbCommand Drop(IIndex index);
        IDbCommand Drop(ITable table);
        IDbCommand Drop(ITrigger trigger);
        IDbCommand Drop(IView view);

        IDbCommand Select(ISelect select);
        IDbCommand Insert(IInsert insert);
        IDbCommand Update(IUpdate update);
        IDbCommand Delete(IDelete delete);

        ISet<IFunction> Functions();
    }
}
