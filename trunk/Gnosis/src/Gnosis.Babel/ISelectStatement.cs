using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Babel
{
    public interface ISelectStatement<TModel, TCriteria> : IStatement //, IFilterable<TModel, TCriteria>
        where TModel : IModel
        where TCriteria : ICriteria
    {
        ISelectStatement<TModel, TCriteria> Column(Expression<Func<TModel, object>> property);
        ISelectStatement<TModel, TCriteria> Columns(IEnumerable<Expression<Func<TModel, object>>> properties);
    }
}
