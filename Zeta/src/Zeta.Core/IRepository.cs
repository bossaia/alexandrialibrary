using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Zeta.Core
{
    public interface IRepository
    {
        T Find<T>(Guid id) where T : IModel;
        void Delete<T>(T target);
        T[] Query<T>(Expression<Func<T, bool>> where);
        T FindBy<T, U>(Expression<Func<T, U>> expression, U search) where T : class;
        T FindBy<T>(Expression<Func<T, bool>> where);
        void Save<T>(T target);
    }
}
