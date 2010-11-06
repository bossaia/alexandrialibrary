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

        protected static DateTime GetDateTime(object value)
        {
            return GetDateTime(value, DateTime.MinValue);
        }

        protected static DateTime GetDateTime(object value, DateTime defaultValue)
        {
            if (value == null)
                return defaultValue;
            else
                return DateTime.Parse((string)value);
        }

        protected static decimal GetDecimal(object value)
        {
            return GetDecimal(value, 0M);
        }

        protected static decimal GetDecimal(object value, decimal defaultValue)
        {
            if (value == null)
                return defaultValue;
            else
                return decimal.Parse((string)value);
        }

        protected static int GetInteger(object value)
        {
            return GetInteger(value, 0);
        }

        protected static int GetInteger(object value, int defaultValue)
        {
            if (value == null)
                return defaultValue;
            else
                return int.Parse((string)value);
        }

        protected static string GetString(object value)
        {
            return GetString(value, string.Empty);
        }

        protected static string GetString(object value, string defaultValue)
        {
            if (value == null)
                return defaultValue;
            else
                return value.ToString();
        }

        public void Map(T model, IDataRecord record)
        {
            foreach (var pair in _actions)
                pair.Value(model, record[pair.Key]);
        }
    }
}
