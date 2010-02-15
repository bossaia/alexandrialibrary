using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public class ChangeSet
		: IChangeSet
	{
		public ChangeSet(ChangeType changeType, IEnumerable<Change> changes)
		{
			_changeType = changeType;
			_changes = changes ?? new List<Change>();
		}

		private readonly ChangeType _changeType;
		private readonly IEnumerable<Change> _changes;

		#region IChangeSet Members

		public ChangeType ChangeType
		{
			get { return _changeType; }
		}

		public string Entity
		{
			get { throw new NotImplementedException(); }
		}

		public long Id
		{
			get { throw new NotImplementedException(); }
		}

		public IChangeSet Parent
		{
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<IChangeSet> Children
		{
			get { throw new NotImplementedException(); }
		}

		public void AddChild(IChangeSet child)
		{
			throw new NotImplementedException();
		}

		public void SetId(long id)
		{
			throw new NotImplementedException();
		}

		public void SetParent(IChangeSet parent)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<Change> Members

		public IEnumerator<Change> GetEnumerator()
		{
			return _changes.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return _changes.GetEnumerator();
		}

		#endregion
	}
}
