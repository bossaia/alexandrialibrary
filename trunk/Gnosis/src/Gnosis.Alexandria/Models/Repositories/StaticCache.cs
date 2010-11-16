using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Repositories
{
    public class StaticCache<T> : ICache<T>
        where T : IModel
    {
        public StaticCache()
        {
        }

        private readonly IDictionary<object, T> _map = new Dictionary<object, T>();

        public bool IsEmpty
        {
            get { return _map.Count == 0; }
        }

        public bool IsFull
        {
            get { return false; }
        }

        public T GetOne(object id)
        {
            return (_map.ContainsKey(id)) ? _map[id] : default(T);
        }

        public ICollection<T> GetAll()
        {
            return _map.Values;
        }

        public void Put(T model)
        {
            _map[model.Id] = model;
        }
    }
}
