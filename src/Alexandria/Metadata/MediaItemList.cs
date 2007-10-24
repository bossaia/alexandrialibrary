using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Alexandria.Metadata
{
	public class MediaItemList : IBindingListView
	{
		public MediaItemList()
		{
		}
		
		private BindingList<IMediaItem> bindingList = new BindingList<IMediaItem>();
	
		#region IBindingList Members
		public void AddIndex(PropertyDescriptor property)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object AddNew()
		{
			return null;
		}

		public bool AllowEdit
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool AllowNew
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool AllowRemove
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int Find(PropertyDescriptor property, object key)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool IsSorted
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public event ListChangedEventHandler ListChanged;

		public void RemoveIndex(PropertyDescriptor property)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveSort()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public ListSortDirection SortDirection
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public PropertyDescriptor SortProperty
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool SupportsChangeNotification
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool SupportsSearching
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool SupportsSorting
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		#endregion

		#region IList Members
		public int Add(object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Clear()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool Contains(object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int IndexOf(object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void Insert(int index, object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public bool IsFixedSize
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool IsReadOnly
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public void Remove(object value)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void RemoveAt(int index)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public object this[int index]
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}
		#endregion

		#region ICollection Members
		public void CopyTo(Array array, int index)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int Count
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool IsSynchronized
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public object SyncRoot
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		#endregion

		#region IEnumerable Members
		public System.Collections.IEnumerator GetEnumerator()
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion

		#region IBindingListView Members
		public void ApplySort(ListSortDescriptionCollection sorts)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public string Filter
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}

		public void RemoveFilter()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public ListSortDescriptionCollection SortDescriptions
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool SupportsAdvancedSorting
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public bool SupportsFiltering
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}
		#endregion
	}
}
