using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel.SQLite.Schema
{
    public interface ITrigger : IStatement
    {
    }

    public interface ITrigger<T> : IStatement
    {
        ITriggerType AfterDeleteOn(string table);
        ITriggerType BeforeDeleteOn(string table);
        ITriggerType InsteadOfDeleteOn(string table);

        ITriggerType AfterInsertOn(string table);
        ITriggerType BeforeInsertOn(string table);
        ITriggerType InsteadOfInsertOn(string table);

        ITriggerUpdateOf AfterUpdateOf(Expression<Func<T, object>> expression);
        ITriggerUpdateOf BeforeUpdateOf(Expression<Func<T, object>> expression);
        ITriggerUpdateOf InsteadOfUpdateOf(Expression<Func<T, object>> expression);

        ITriggerType AfterUpdateOn(string table);
        ITriggerType BeforeUpdateOn(string table);
        ITriggerType InsteadOfUpdateOn(string table);
    }
}
