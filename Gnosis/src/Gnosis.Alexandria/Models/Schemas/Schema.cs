using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Gnosis.Alexandria.Models.Interfaces;
using Gnosis.Alexandria.Utilities;

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

        #region Protected Methods

        protected static Tuple<string, bool> Ascending(Expression<Func<T, object>> getter)
        {
            var name = getter.ToName();
            return Ascending(name);
        }

        protected static Tuple<string, bool> Ascending(string name)
        {
            return Tuple.Create<string, bool>(name, true);
        }

        protected static Tuple<string, bool> Descending(Expression<Func<T, object>> getter)
        {
            var name = getter.ToName();
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
            var name = getter.ToName();
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

        protected IField<T> GetField(string name)
        {
            return (_fields.ContainsKey(name))
                       ? _fields[name]
                       : null;
        }

        protected IField<T> GetField(Expression<Func<T, object>> expression)
        {
            return GetField(expression.ToName());
        }

        protected IKey<T> GetKey(string name)
        {
            return (_keys.ContainsKey(name))
                       ? _keys[name]
                       : null;
        }

        #endregion

        public string Name
        {
            get { return _name; }
        }

        public IEnumerable<IField<T>> Fields
        {
            get { return _fields.Values; }
        }

        public IEnumerable<IField<T>> NonPrimaryFields
        {
            get { return _fields.Values.Except(GetField(x => x.Id)); }
        }

        public IEnumerable<IKey<T>> Keys
        {
            get { return _keys.Values; }
        }
    }
}
