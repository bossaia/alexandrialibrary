using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Collections;
using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria
{
	public abstract class EntityBase
		: IEntity
	{
		protected EntityBase(IContext context)
			: this(context, 0)
		{
		}

		protected EntityBase(IContext context, long id)
		{
			_context = context;
			_id = id;
		}

		private readonly long _id;
		private bool _isDeleted;
		private readonly IContext _context;
		private readonly Dictionary<string, Change> _changes = new Dictionary<string, Change>();
		private Set<ILink> _links;
		private Set<ITag> _tags;

		private Set<ILink> LinkSet
		{
			get { return _links ?? (_links = new Set<ILink>(_context.Links.GetBySource(this))); }
		}

		private Set<ITag> TagSet
		{
			get { return _tags ?? (_tags = new Set<ITag>(_context.Tags.GetByEntity(this))); }
		}

		protected IContext Context
		{
			get { return _context; }
		}

		protected void AddChange(Change change)
		{
			_changes[change.Property] = change;
		}

		#region IEntity Members

		public long Id
		{
			get { return _id; }
		}

		public bool IsChanged()
		{
			return _changes.Count > 0;
		}

		public bool IsDeleted()
		{
			return _isDeleted;
		}

		public bool IsNew()
		{
			return _id == 0;
		}

		public virtual IChangeSet Changes()
		{
			ChangeType changeType = ChangeType.None;
			if (IsNew())
				changeType = ChangeType.Create;
			else if (IsDeleted())
				changeType = ChangeType.Delete;
			else if (IsChanged())
				changeType = ChangeType.Update;

			IChangeSet changeSet = new ChangeSet(changeType, _changes.Values);

			if (_links != null)
			{
				foreach (ILink link in _links)
					changeSet.AddChild(link.Changes());
			}

			if (_tags != null)
			{
				foreach (ITag tag in _tags)
					changeSet.AddChild(tag.Changes());
			}

			return changeSet;
		}

		public virtual void Delete()
		{
			if (IsNew())
				throw new InvalidOperationException("Cannot delete a new entity");

			_isDeleted = true;

			if (_links != null)
			{
				foreach (ILink link in _links)
					link.Delete();
			}

			if (_tags != null)
			{
				foreach (ITag tag in _tags)
					tag.Delete();
			}
		}

		public IEnumerable<ILink> Links()
		{
			return LinkSet;
		}

		public IEnumerable<ITag> Tags()
		{
			return TagSet;
		}

		public void AddLink(ILink link)
		{
			LinkSet.Add(link);
		}

		public void RemoveLink(ILink link)
		{
			LinkSet.Remove(link);
		}

		public void AddTag(ITag tag)
		{
			TagSet.Add(tag);
		}

		public void RemoveTag(ITag tag)
		{
			TagSet.Remove(tag);
		}

		#endregion
	}
}
