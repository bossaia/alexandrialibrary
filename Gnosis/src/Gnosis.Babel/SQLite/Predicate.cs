using System;
using System.Linq.Expressions;

namespace Gnosis.Babel.SQLite
{
    public class Predicate<TInterface, TConcrete> : Statement, IPredicate<TInterface>
        where TInterface : IStatement
        where TConcrete : Statement, TInterface, new()
    {
        #region Private Constants

        private const string OpEqualTo = "=";
        private const string OpNotEqualTo = "<>";
        private const string OpLessThan = "<";
        private const string OpLessThanOrEqualTo = "<=";
        private const string OpGreaterThan = ">";
        private const string OpGreaterThanOrEqualTo = ">=";
        private const string OpInPrefix = "in (";
        private const string OpNotInPrefix = "not in (";
        private const string OpInSuffix = ")";
        private const string OpNull = "isnull";
        private const string OpNotNull = "not null";
        private const string OpLike = "like";
        private const string OpNotLike = "not like";

        #endregion

        public TInterface IsEqualTo(string expression)
        {
            AppendWord(OpEqualTo);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsEqualTo(string name, object value)
        {
            AppendWord(OpEqualTo);
            return AppendParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsEqualTo<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpEqualTo);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpEqualTo);
            return AppendParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsNotEqualTo(string expression)
        {
            AppendWord(OpNotEqualTo);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsNotEqualTo(string name, object value)
        {
            AppendWord(OpNotEqualTo);
            return AppendParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsNotEqualTo<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpNotEqualTo);
            return AppendWord<TInterface, TConcrete>(expression.ToString());
        }

        public TInterface IsNotEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpNotEqualTo);
            return AppendParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsLessThan(string expression)
        {
            AppendWord(OpLessThan);
            return AppendWord<TInterface, TConcrete>(expression);
        }

        public TInterface IsLessThan(string name, object value)
        {
            AppendWord(OpLessThan);
            return AppendParameter<TInterface, TConcrete>(name, value);
        }

        public TInterface IsLessThan<T>(Expression<Func<T, object>> expression)
        {
            AppendWord(OpLessThan);
            return AppendWord<TInterface, TConcrete>(expression.ToName());
        }

        public TInterface IsLessThan<T>(Expression<Func<T, object>> expression, object value)
        {
            AppendWord(OpLessThan);
            return AppendParameter<TInterface, TConcrete>(expression.ToName(), value);
        }

        public TInterface IsLessThanOrEqualTo(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLessThanOrEqualTo(string name, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLessThanOrEqualTo<T>(Expression<Func<T, object>> expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThan(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThan(string expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThan<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThan<T>(System.Linq.Expressions.Expression<Func<T, object>> expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThanOrEqualTo(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThanOrEqualTo(string expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThanOrEqualTo<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsGreaterThanOrEqualTo<T>(System.Linq.Expressions.Expression<Func<T, object>> expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLike(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLike(string expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLike<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsLike<T>(System.Linq.Expressions.Expression<Func<T, object>> expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNotLike(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNotLike(string expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNotLike<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNotLike<T>(System.Linq.Expressions.Expression<Func<T, object>> expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsIn(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsIn(string expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNotIn(string expression)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNotIn(string expression, object value)
        {
            throw new NotImplementedException();
        }

        public TInterface IsNull
        {
            get { throw new NotImplementedException(); }
        }

        public TInterface IsNotNull
        {
            get { throw new NotImplementedException(); }
        }
    }
}
