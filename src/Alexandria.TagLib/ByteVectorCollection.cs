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

namespace Alexandria.TagLib
{
	public class ByteVectorCollection : IList<ByteVector>
	{
		#region Constructors
		public ByteVectorCollection()
		{
		}

		public ByteVectorCollection(ByteVector vector)
		{
			Add(vector);
		}
		#endregion
		
		#region Private Fields
		private List<ByteVector> data = new List<ByteVector>();
		#endregion
		
		#region Public Properties
		public bool IsReadOnly
		{
			get { return false; }
		}
		
		//public bool IsFixedSize
		//{
			//get { return false; }
		//}

		ByteVector IList<ByteVector>.this[int index]
		{
			get { return this[index]; }
			set { this[index] = value; }
		}

		public ByteVector this[int index]
		{
			get { return data[index]; }
			set { data[index] = value; }
		}

		public int Count
		{
			get { return data.Count; }
		}

		//public bool IsSynchronized
		//{
			//get { return false; }
		//}

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
		void ICollection<ByteVector>.Add(ByteVector value)
		{
			Add(value);
		}

		public void Add(ByteVector value)
		{
			data.Add(value);
		}

		public void Clear()
		{
			data.Clear();
		}

		//bool IList<ByteVector>.Contains(ByteVector value)
		//{
		//return Contains(value);
		//}

		public bool Contains(ByteVector item)
		{
			return IndexOf(item) != -1;
		}

		int IList<ByteVector>.IndexOf(ByteVector item)
		{
			return IndexOf(item);
		}

		public int IndexOf(ByteVector item)
		{
			for (int i = 0; i < data.Count; i++)
				if (item == data[i]) return i;

			return -1;
		}

		void IList<ByteVector>.Insert(int index, ByteVector value)
		{
			Insert(index, value);
		}

		public void Insert(int index, ByteVector value)
		{
			data.Insert(index, value);
		}

		bool ICollection<ByteVector>.Remove(ByteVector item)
		{
			return Remove(item);
		}

		public bool Remove(ByteVector item)
		{
			return data.Remove(item);
		}

		public void RemoveAt(int index)
		{
			data.RemoveAt(index);
		}

		void ICollection<ByteVector>.CopyTo(ByteVector[] vectors, int index)
		{
			data.CopyTo(vectors, index);
		}

		public void SortedInsert(ByteVector vector, bool unique)
		{
			int i = 0;
			for (; i < data.Count; i++)
			{
				if (vector == (ByteVector)data[i] && unique)
					return;

				if (vector >= (ByteVector)data[i])
					break;
			}

			Insert(i + 1, vector);
		}

		public void SortedInsert(ByteVector vector)
		{
			SortedInsert(vector, false);
		}

		public ByteVector ToByteVector(ByteVector separator)
		{
			ByteVector v = new ByteVector();

			for (int i = 0; i < Count; i++)
			{
				if (i != 0)
					v.Add(separator);

				v.Add(this[i]);
			}

			return v;
		}

		public ByteVector ToByteVector()
		{
			return ToByteVector(" ");
		}
		#endregion
		
		#region Public Static Methods
		public static ByteVectorCollection Split(ByteVector vector, ByteVector pattern, int alignment, int maximum)
		{
			if (vector != null)
			{
				if (pattern != null)
				{
					ByteVectorCollection list = new ByteVectorCollection();
					int previousOffset = 0;
					for (int offset = vector.Find(pattern, 0, alignment);
						 offset != -1 && (maximum == 0 || maximum > list.Count + 1);
						 offset = vector.Find(pattern, offset + pattern.Count, alignment))
					{
						list.Add(vector.Mid(previousOffset, offset - previousOffset));
						previousOffset = offset + pattern.Count;
					}

					if (previousOffset < vector.Count)
						list.Add(vector.Mid(previousOffset, vector.Count - previousOffset));

					return list;
				}
				else throw new ArgumentNullException("pattern");
			}
			else throw new ArgumentNullException("vector");
		}

		public static ByteVectorCollection Split(ByteVector vector, ByteVector pattern, int alignment)
		{
			return Split(vector, pattern, alignment, 0);
		}

		public static ByteVectorCollection Split(ByteVector vector, ByteVector pattern)
		{
			return Split(vector, pattern, 1);
		}
		#endregion
		
		#region IEnumerable Members
		IEnumerator IEnumerable.GetEnumerator()
		{
			return data.GetEnumerator();
		}

		public IEnumerator<ByteVector> GetEnumerator()
		{
			foreach (ByteVector vector in data)
				yield return vector;
		}
		#endregion
	}
}
