using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IRepository<T>
    {
        ISet<T> Get();
        ISet<T> Get(IExpression<T, bool> predicate);
    }
}
