using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IRepository<T>
    {
        T GetByKey(string name, ITuple key);
        IEnumerable<T> List();
        IEnumerable<T> List(IExpression<T, bool> predicate);
    }
}
