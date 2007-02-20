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
	public class IntCollection : IList<int>
	{
		#region Constructors
		public IntCollection()
		{
		}

		public IntCollection(int number)
		{
			Add(number);
		}
		public IntCollection(IntCollection numbers)
		{
			Add(numbers);
		}
		public IntCollection(int[] numbers)
		{
			Add(numbers);
		}
		#endregion
		
		#region Private Fields
		private List<int> data = new List<int>();
		private bool isReadOnly;
		private bool isFixedSize;
		private bool isSynchronized;
		#endregion
		
		#region Public Properties
		public bool IsReadOnly
		{
			get { return isReadOnly; }
			protected set { this.isReadOnly = value; }
		}

		public bool IsFixedSize
		{
			get { return isFixedSize; }
			protected set { this.isFixedSize = value; }
		}

		int IList<int>.this[int index]
		{
			get { return this[index]; }
			set { this[index] = value; }
		}

		public int this[int index]
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
			get { return this.isSynchronized; }
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
		void ICollection<int>.Add(int value)
		{
			Add(value);
		}

		public void Add(int value)
		{
			data.Add(value);
		}

		public void Clear()
		{
			data.Clear();
		}

		bool ICollection<int>.Contains(int value)
		{
			return Contains(value);
		}

		public bool Contains(int value)
		{
			return IndexOf(value) != -1;
		}

		int IList<int>.IndexOf(int value)
		{
			return IndexOf((int)value);
		}

		public int IndexOf(int value)
		{
			for (int i = 0; i < data.Count; i++)
				if (value == data[i]) return i;

			return -1;
		}

		void IList<int>.Insert(int index, int value)
		{
			Insert(index, value);
		}

		public void Insert(int index, int value)
		{
			data.Insert(index, value);
		}

		bool ICollection<int>.Remove(int value)
		{
			return Remove(value);
		}

		public bool Remove(int value)
		{
			return data.Remove(value);
		}

		public void RemoveAt(int index)
		{
			data.RemoveAt(index);
		}

		void ICollection<int>.CopyTo(int[] numbers, int index)
		{
			data.CopyTo(numbers, index);
		}

		public void Add(IntCollection numbers)
		{
			if (numbers != null)
				data.AddRange(numbers);
		}

		public void Add(int[] numbers)
		{
			if (numbers != null)
				data.AddRange(numbers);
		}

		public void SortedInsert(int number, bool unique)
		{
			int i = 0;
			for (; i < data.Count; i++)
			{
				if (number == (int)data[i] && unique)
					return;

				if (number <= (int)data[i])
					break;
			}

			Insert(i, number);
		}

		public void SortedInsert(int number)
		{
			SortedInsert(number, false);
		}

		public int[] ToArray()
		{
			return data.ToArray();
		}
		#endregion
		
		#region IEnumerable Members
		IEnumerator IEnumerable.GetEnumerator()
		{
			return data.GetEnumerator();
		}

		public IEnumerator<int> GetEnumerator()
		{
			return data.GetEnumerator();
		}
		#endregion
	}
}
