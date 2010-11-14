using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Gnosis.Alexandria.Models.Interfaces
{
    public interface IClause
    {
        string Name { get; }

        void Append(params string[] tokens);
        void Append<T>(Expression<Func<T, object>> expression) where T : IModel;
        void Append<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
        void AppendFormat(string format, params object[] args);

        string GetName<T>(Expression<Func<T, object>> expression) where T : IModel;
        string GetName<T>(Expression<Func<T, object>> expression, string alias) where T : IModel;
    }
}
