using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IInsertBuilder : ICommandBuilder
    {
        IInsertBuilder Insert(string table);

        IInsertBuilder OrRollback { get; }
        IInsertBuilder OrAbort { get; }
        IInsertBuilder OrReplace { get; }
        IInsertBuilder OrFail { get; }
        IInsertBuilder OrIgnore { get; }

        IInsertBuilder Set<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IInsertBuilder Set(string name, object value);
        IInsertBuilder SetAll<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel;

        IInsertBuilder AddParameter(string name, object value);
        IInsertBuilder AddParameter<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
    }
}
