using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IInsertBuilder : ICommandBuilder
    {
        IInsertBuilder Insert { get; }
        IInsertBuilder OrRollback { get; }
        IInsertBuilder OrAbort { get; }
        IInsertBuilder OrReplace { get; }
        IInsertBuilder OrFail { get; }
        IInsertBuilder OrIgnore { get; }
        
        IInsertBuilder Into(string table);

        IInsertBuilder ColumnToValue<T>(Expression<Func<T, object>> expression, T model) where T : IModel;
        IInsertBuilder ColumnToValue(string name, object value);
        IInsertBuilder ColumnsToValues<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel;

        IInsertBuilder AddParameter(string name, object value);
        IInsertBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel;

        IInsertBuilder SetCallback<T>(Action<IModel, object> callback, IModel model) where T : IModel;
    }
}
