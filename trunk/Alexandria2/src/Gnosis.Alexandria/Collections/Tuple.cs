using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Collections
{
	public class Tuple<T>
		: ITuple<T>
		where T : IEquatable<T>, IComparable<T>
	{
		public Tuple()
		{
		}

		public Tuple(IEnumerable<T> items)
		{
			AddRange(items);
		}

		private SortedList<int, T> _list = new SortedList<int, T>();

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return _list.Values.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _list.Values.GetEnumerator();
		}

		#endregion

		public void Add(T item)
		{
			_list.Add(item.GetHashCode(), item);
		}

		public void AddRange(IEnumerable<T> items)
		{
			if (items != null)
			{
				foreach (T item in items)
				{
					Add(item);
				}
			}
		}

		public void Remove(T item)
		{
			_list.Remove(item.GetHashCode());
		}
	}
}
