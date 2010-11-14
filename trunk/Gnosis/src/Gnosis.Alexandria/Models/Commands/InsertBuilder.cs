using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

namespace Gnosis.Alexandria.Models.Commands
{
    public class InsertBuilder : CommandBuilder, IInsertBuilder
    {
        public InsertBuilder(IFactory<ICommand> factory)
            : base(factory)
        {
            AddSpaceDelimitedClause(Clauses.Insert);
            AddSpaceDelimitedClause(Clauses.Into);
            AddCommaDelimitedClause(Clauses.Columns, Constants.OpenParen, Constants.CloseParen);
            AddSpaceDelimitedClause(Clauses.Values);
            AddCommaDelimitedClause(Clauses.ColumnAssignments, Constants.OpenParen, Constants.CloseParen);
        }

        public IInsertBuilder Insert
        {
            get
            {
                SetCurrentClause(Clauses.Insert);
                Append(Constants.Update);
                return this;
            }
        }

        public IInsertBuilder OrAbort
        {
            get
            {
                Append(Constants.OrAbort);
                return this;
            }
        }

        public IInsertBuilder OrFail
        {
            get
            {
                Append(Constants.OrFail);
                return this;
            }
        }

        public IInsertBuilder OrIgnore
        {
            get
            {
                Append(Constants.OrIgnore);
                return this;
            }
        }

        public IInsertBuilder OrReplace
        {
            get
            {
                Append(Constants.OrReplace);
                return this;
            }
        }

        public IInsertBuilder OrRollback
        {
            get
            {
                Append(Constants.OrRollback);
                return this;
            }
        }

        public IInsertBuilder Into(string table)
        {
            SetCurrentClause(Clauses.Into);
            Append(Constants.Into, table);
            SetCurrentClause(Clauses.Values);
            Append(Constants.Values);
            return this;
        }

        public IInsertBuilder ColumnToValue<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            var getter = expression.Compile();
            ColumnToValue(GetName(expression), getter(model));
            return this;
        }

        public IInsertBuilder ColumnToValue(string name, object value)
        {
            SetCurrentClause(Clauses.Columns);
            Append(name);
            SetCurrentClause(Clauses.ColumnAssignments);
            AppendParameter(name, value, Constants.ColumnAssignment);
            return this;
        }

        public IInsertBuilder ColumnsToValues<T>(IEnumerable<Expression<Func<T, object>>> expressions, T model) where T : IModel
        {
            expressions.Each<Expression<Func<T, object>>>(x => ColumnToValue(x, model));
            return this;
        }

        public IInsertBuilder AddParameter(string name, object value)
        {
            AddParameter(name, value);
            return this;
        }

        public IInsertBuilder AddParameter<T>(Expression<Func<T, object>> expression, object value) where T : IModel
        {
            AddParameter(expression, value);
            return this;
        }

        public IInsertBuilder AddParameter<T>(Expression<Func<T, object>> expression, T model) where T : IModel
        {
            AddParameter(expression, model);
            return this;
        }

        public IInsertBuilder SetCallback<T>(Action<IModel, object> callback, IModel model) where T : IModel
        {
            SetCallback(callback, model);
            Append(Constants.SelectLastInsertedId);
            return this;
        }
    }
}
