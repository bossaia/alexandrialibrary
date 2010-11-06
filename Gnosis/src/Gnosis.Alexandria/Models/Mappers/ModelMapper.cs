using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Mappers
{
    public abstract class ModelMapper<T>
        where T : IModel
    {
        private readonly IDictionary<string, Action<T, object>> _actions = new Dictionary<string, Action<T, object>>();

        protected void AddAction(string name, Action<T, object> action)
        {
            _actions.Add(name, action);
        }

        protected static string GetString(object value)
        {
            if (value == null)
                return string.Empty;
            else
                return value.ToString();
        }

        protected static DateTime GetDateTime(object value)
        {
            if (value == null)
                return DateTime.MinValue;
            else
                return DateTime.Parse((string)value);
        }

        public void Map(T model, IDataRecord record)
        {
            foreach (var pair in _actions)
                pair.Value(model, record[pair.Key]);
        }
    }
}
