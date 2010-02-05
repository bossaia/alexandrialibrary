using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public class Set<T>
		: ISet<T>
		where T : IEquatable<T>
	{
		public Set()
		{
		}

		public Set(IEnumerable<T> items)
		{
			AddRange(items);
		}

		private Dictionary<int, T> _map = new Dictionary<int, T>();

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return _map.Values.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _map.Values.GetEnumerator();
		}

		#endregion

		public void Add(T item)
		{
			if (item != null)
				_map.Add(item.GetHashCode(), item);
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
			if (item != null)
				_map.Remove(item.GetHashCode());
		}
	}
}
