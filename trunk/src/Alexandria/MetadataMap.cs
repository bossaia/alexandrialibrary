using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class MetadataMap<K, V> : IDictionary<K, V>
	{
		#region Inner Classes
		private class CaseInsensitiveComparer : IEqualityComparer<K>
		{
			#region IEqualityComparer<K> Members
			public bool Equals(K x, K y)
			{
				if (x != null && y != null)
				{
					return (x.ToString().ToLowerInvariant() == y.ToString().ToLowerInvariant());
				}
				else return false;
			}

			public int GetHashCode(K obj)
			{
				return obj.GetHashCode();
			}
			#endregion
		}
		#endregion
	
		#region Constructors
		public MetadataMap()
		{
		}
		
		public MetadataMap(IIdentifier id, string name, ILocation location)
		{
			//this.guid = guid;
			this.name = name;
			//this.uri = uri;
		}
		#endregion
		
		#region Private Fields
		//private Guid guid;
		private string name;
		//private Uri uri;
		private IDictionary<K, V> map = new Dictionary<K,V>(new CaseInsensitiveComparer());
		#endregion
		
		#region Public Properties
		public IIdentifier Id
		{
			get { return null; }
		}
		
		public string Name
		{
			get { return name; }
		}
		
		public ILocation Location
		{
			get { return null; }
		}
		#endregion
		
		#region IDictionary<K,V> Members
		public void Add(K key, V value)
		{
			map.Add(key, value);
		}
		
		public bool ContainsKey(K key)
		{
			return map.ContainsKey(key);
		}

		public ICollection<K> Keys
		{
			get { return map.Keys; }
		}

		public bool Remove(K key)
		{
			return map.Remove(key);
		}

		public bool TryGetValue(K key, out V value)
		{
			return map.TryGetValue(key, out value);
		}

		public ICollection<V> Values
		{
			get { return map.Values; }
		}

		public V this[K key]
		{
			get { return map[key]; }
			set { map[key] = value; }
		}
		#endregion

		#region ICollection<KeyValuePair<K,V>> Members
		public void Add(KeyValuePair<K, V> item)
		{
			map.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			map.Clear();
		}

		public bool Contains(KeyValuePair<K, V> item)
		{
			return map.Contains(item);
		}

		public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
		{
			map.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get { return map.Count; }
		}

		public bool IsReadOnly
		{
			get { return map.IsReadOnly; }
		}

		public bool Remove(KeyValuePair<K, V> item)
		{
			return map.Remove(item.Key);
		}
		#endregion

		#region IEnumerable<KeyValuePair<K,V>> Members
		public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
		{
			foreach(KeyValuePair<K, V> item in map)
				yield return item;
		}
		#endregion

		#region IEnumerable Members
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			foreach (KeyValuePair<K, V> item in map)
				yield return item;
		}
		#endregion
	}
}
