using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public class LinkCollection : ILinkCollection
	{
		public LinkCollection()
		{
		}

		#region ILinkCollection Members

		public ILinkCollection FilterByLinkType<T>()
			where T : ILinkType
		{
			throw new NotImplementedException();
		}

		public ISubjectLinkCollection<X> FilterBySubjectType<X>()
			where X : IEntityType
		{
			throw new NotImplementedException();
		}

		public IObjectLinkCollection<Y> FilterByObjectType<Y>()
			where Y : IEntityType
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IList<ILink> Members

		public int IndexOf(ILink item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, ILink item)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public ILink this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region ICollection<ILink> Members

		public void Add(ILink item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(ILink item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(ILink[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public bool Remove(ILink item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<ILink> Members

		public IEnumerator<ILink> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
