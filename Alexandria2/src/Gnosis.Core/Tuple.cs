using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Tuple<T> :
        ITuple<T>
    {
        public Tuple(Func<T, T, bool> equalityFunction, IEnumerable<T> items)
        {
            _equalityFunction = equalityFunction;

            if (items != null)
            {
                foreach (T item in items)
                    _list.Add(item);
            }
        }

        private List<T> _list = new List<T>();
        private Func<T, T, bool> _equalityFunction;

        #region ITuple<T> Members

        public Func<T, T, bool> EqualityFunction
        {
            get { return _equalityFunction; }
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool Contains(T item)
        {
            foreach (T existingItem in _list)
                if (_equalityFunction(existingItem, item))
                    return true;

            return false;
        }

        public ITuple<T> Add(T item)
        {
            List<T> items = new List<T>();

            foreach (T existingItem in _list)
                items.Add(existingItem);

            items.Add(item);

            return new Tuple<T>(_equalityFunction, items);
        }

        public ITuple<T> Remove(T item)
        {
            List<T> items = new List<T>(_list);

            foreach (T existingItem in items)
            {
                if (_equalityFunction(existingItem, item))
                {
                    items.Remove(existingItem);
                    break;
                }
            }

            return new Tuple<T>(_equalityFunction, items);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}
