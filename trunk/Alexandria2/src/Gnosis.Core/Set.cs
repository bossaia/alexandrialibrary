using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class Set<T> :
        ISet<T>
    {
        public Set(Func<T, T, bool> equalityFunction, IEnumerable<T> items)
        {
            if (equalityFunction == null)
                throw new ArgumentNullException("equalityFunction");

            _equalityFunction = equalityFunction;

            if (items != null)
            {
                foreach (T item in items)
                    AddIfUnique(_list, item);
            }
        }

        private Func<T, T, bool> _equalityFunction;
        private List<T> _list = new List<T>();

        private void AddIfUnique(List<T> list, T item)
        {
            foreach (T existingItem in list)
                if (_equalityFunction(item, existingItem))
                    return;

            list.Add(item);
        }

        #region ISet<T> Members

        public Func<T, T, bool> EqualityFunction
        {
            get { throw new NotImplementedException(); }
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool Contains(T item)
        {
            foreach (T existingItem in _list)
                if (_equalityFunction(existingItem, item))
                    return true;

            return false;
        }

        public ISet<T> Add(T item)
        {
            List<T> items = new List<T>(_list);

            AddIfUnique(items, item);

            return new Set<T>(_equalityFunction, items);
        }

        public ISet<T> Remove(T item)
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

            return new Set<T>(_equalityFunction, items);
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
