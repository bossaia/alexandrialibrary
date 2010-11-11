using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Schemas
{
    public abstract class Schema<T> : ISchema<T>
        where T : IModel
    {
        protected Schema(string name)
        {
            _name = name;
            AddField(x => x.Id, (x, y) => x.Initialize(y));
            AddPrimaryKey(Ascending(x => x.Id));
        }

        private readonly string _name;
        private readonly IDictionary<string, IField<T>> _fields = new Dictionary<string, IField<T>>();
        private readonly IDictionary<string, IKey<T>> _keys = new Dictionary<string, IKey<T>>();

        private void AddKey(KeyType keyType, IEnumerable<Tuple<string, bool>> keyFields)
        {
            var keyName = _name + keyFields.Aggregate(string.Empty, (x, y) => x += "_" + y.Item1) + "_key";
            _keys[keyName] = new Key<T>(keyName, keyType, keyFields);
        }

        private static string GetName(Expression<Func<T, object>> getter)
        {
            return GetNameForExpression(getter.Body);
        }

        private static string GetNameForExpression(Expression expression)
        {
            MemberExpression memberExpression = null;
            if (expression.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression as MemberExpression;
            }

            if (memberExpression == null)
                throw new ArgumentException("Getter is not a valid member expression");

            return memberExpression.Member.Name;
        }

        #region Add Methods

        protected static Tuple<string, bool> Ascending(Expression<Func<T, object>> getter)
        {
            var name = GetName(getter);
            return Ascending(name);
        }

        protected static Tuple<string, bool> Ascending(string name)
        {
            return Tuple.Create<string, bool>(name, true);
        }

        protected static Tuple<string, bool> Descending(Expression<Func<T, object>> getter)
        {
            var name = GetName(getter);
            return Descending(name);
        }

        protected static Tuple<string, bool> Descending(string name)
        {
            return Tuple.Create<string, bool>(name, false);
        }

        protected void AddField(Expression<Func<T, object>> getter)
        {
            AddField(getter, null);
        }

        protected void AddField(Expression<Func<T, object>> getter, Action<T, object> setter)
        {
            var name = GetName(getter);
            AddField(getter, setter, name);
        }

        protected void AddField(Expression<Func<T, object>> getter, Action<T, object> setter, string name)
        {
            _fields[name] = new Field<T>(name, getter, setter);
        }

        protected void AddPrimaryKey(params Tuple<string,bool >[] keyFields)
        {
            AddKey(KeyType.PrimaryKey, keyFields);
        }

        protected void AddUniqueKey(params Tuple<string,bool>[] keyFields)
        {
            AddKey(KeyType.UniqueKey, keyFields);
        }

        protected void AddKey(params Tuple<string, bool>[] keyFields)
        {
            AddKey(KeyType.Key, keyFields);
        }

        #endregion

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<KeyValuePair<string, IField<T>>> Fields
        {
            get { return _fields; }
        }

        public IField<T> GetField(string name)
        {
            return (_fields.ContainsKey(name))
                       ? _fields[name]
                       : null;
        }

        public IEnumerable<KeyValuePair<string, IKey<T>>> Keys
        {
            get { return _keys; }
        }
    }
}
