/***************************************************************************
    copyright            : (C) 2005 by Brian Nickel
    email                : brian.nickel@gmail.com
 ***************************************************************************/

/***************************************************************************
 *   This library is free software; you can redistribute it and/or modify  *
 *   it  under the terms of the GNU Lesser General Public License version  *
 *   2.1 as published by the Free Software Foundation.                     *
 *                                                                         *
 *   This library is distributed in the hope that it will be useful, but   *
 *   WITHOUT ANY WARRANTY; without even the implied warranty of            *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU     *
 *   Lesser General Public License for more details.                       *
 *                                                                         *
 *   You should have received a copy of the GNU Lesser General Public      *
 *   License along with this library; if not, write to the Free Software   *
 *   Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  *
 *   USA                                                                   *
 ***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;

namespace AlexandriaOrg.Alexandria.TagLib
{
	public class StringCollection : IList<string>
	{
		#region Constructors
		public StringCollection()
		{
		}

		public StringCollection(string text)
		{
			Add(text);
		}
		public StringCollection(StringCollection text)
		{
			Add(text);
		}
		public StringCollection(string[] text)
		{
			Add(text);
		}

		public StringCollection(IList<string> text)
		{
			Add(text);
		}

		public StringCollection(ByteVectorCollection text, StringType type)
		{
			foreach(ByteVector vector in text)
				Add(vector.ToString(type));
		}

		public StringCollection(ByteVectorCollection text) : this(text, StringType.UTF8)
		{
		}
		#endregion
		
		#region Private Fields
		private List<string> data = new List<string>();
		private bool isReadOnly;
		private bool isFixedSize;
		private bool isSynchronized;
		#endregion
		
		#region Public Properties
		public bool IsReadOnly
		{
			get { return this.isReadOnly; }
			protected set { this.isReadOnly = value; }
		}

		public bool IsFixedSize
		{
			get { return this.isFixedSize; }
			protected set { this.isFixedSize = value; }
		}

		string IList<string>.this[int index]
		{
			get { return this[index]; }
			set { this[index] = (string)value; }
		}

		public string this[int index]
		{
			get { return data[index]; }
			set { data[index] = value; }
		}

		public int Count
		{
			get { return data.Count; }
		}

		public bool IsSynchronized
		{
			get { return isSynchronized; }
			protected set { this.isSynchronized = value; }
		}

		public object SyncRoot
		{
			get { return this; }
		}

		public bool IsEmpty
		{
			get { return Count == 0; }
		}
		#endregion
		
		#region Public Methods
		void ICollection<string>.Add(string value)
		{
			Add(value);
		}

		public void Add(string value)
		{
			data.Add(value);
		}

		public void Clear()
		{
			data.Clear();
		}

		bool ICollection<string>.Contains(string value)
		{
			return Contains(value);
		}

		public bool Contains(string value)
		{
			return IndexOf(value) != -1;
		}

		int IList<string>.IndexOf(string value)
		{
			return IndexOf((string)value);
		}

		public int IndexOf(string value)
		{
			for (int i = 0; i < data.Count; i++)
				if (value == data[i]) return i;

			return -1;
		}

		void IList<string>.Insert(int index, string value)
		{
			Insert(index, value);
		}

		public void Insert(int index, string value)
		{
			data.Insert(index, value);
		}

		bool ICollection<string>.Remove(string value)
		{
			return Remove(value);
		}

		public bool Remove(string value)
		{
			return data.Remove(value);
		}

		public void RemoveAt(int index)
		{
			data.RemoveAt(index);
		}

		void ICollection<string>.CopyTo(string[] array, int index)
		{
			data.CopyTo(array, index);
		}

		public void Add(StringCollection text)
		{
			if (text != null)
				data.AddRange(text);
		}

		public void Add(string[] text)
		{
			if (text != null)
				data.AddRange(text);
		}
		
		public void Add(IList<string> text)
		{
			if (text != null)
				data.AddRange(text);
		}

		public void SortedInsert(string text, bool unique)
		{
			int i = 0;
			for (; i < data.Count; i++)
			{
				if (text == (string)data[i] && unique)
					return;

				if (text.CompareTo((string)data[i]) <= 0)
					break;
			}
			Insert(i, text);
		}

		public void SortedInsert(string text)
		{
			SortedInsert(text, false);
		}

		public string ToString(string separator)
		{
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			//string field = string.Empty;

			for (int i = 0; i < Count; i++)
			{
				if (i != 0) builder.Append(separator);
				builder.Append(this[i]);
			}

			return builder.ToString();
		}

		public override string ToString()
		{
			return ToString(", ");
		}

		public string[] ToArray()
		{
			return data.ToArray();
		}
		
		public IList<string> ToList()
		{
			return data;
		}
		#endregion
		
		#region Public Static Methods
		public static StringCollection Split(string text, string pattern)
		{
			if (text != null)
			{
				if (pattern != null)
				{
					StringCollection collection = new StringCollection();

					int previousPosition = 0;
					int position = text.IndexOf(pattern, 0);

					while (position != -1)
					{
						collection.Add(text.Substring(previousPosition, position - previousPosition));
						previousPosition = position + pattern.Length;

						position = text.IndexOf(pattern, previousPosition);
					}

					collection.Add(text.Substring(previousPosition));
					return collection;
				}
				else throw new ArgumentNullException("pattern");
			}
			else throw new ArgumentNullException("text");
		}
		#endregion
		
		#region IEnumerable Members
		IEnumerator IEnumerable.GetEnumerator()
		{
			return data.GetEnumerator();
		}

		public System.Collections.Generic.IEnumerator<string> GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
