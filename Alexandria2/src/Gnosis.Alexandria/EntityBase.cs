using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public abstract class EntityBase
		: IEntity
	{
		protected EntityBase(ILinkRepository linkRepository, ITagRepository tagRepository)
			: this(linkRepository, tagRepository, 0)
		{
		}

		protected EntityBase(ILinkRepository linkRepository, ITagRepository tagRepository, long id)
		{
			_linkRepository = linkRepository;
			_tagRepository = tagRepository;
			_id = id;
		}

		private long _id;
		private ILinkRepository _linkRepository;
		private ITagRepository _tagRepository;
		private Set<Link> _links;
		private Set<Tag> _tags;

		private Set<Link> LinkSet
		{
			get { return _links ?? (_links = new Set<Link>(_linkRepository.GetBySource(this))); }
		}

		private Set<Tag> TagSet
		{
			get { return _tags ?? (_tags = new Set<Tag>(_tagRepository.GetByEntity(this))); }
		}

		#region IEntity Members

		public long Id
		{
			get { return _id; }
		}

		public ISet<Link> Links()
		{
			return LinkSet;
		}

		public ISet<Tag> Tags()
		{
			return TagSet;
		}

		public void AddLink(Link link)
		{
			LinkSet.Add(link);
		}

		public void RemoveLink(Link link)
		{
			LinkSet.Remove(link);
		}

		public void AddTag(Tag tag)
		{
			TagSet.Add(tag);
		}

		public void RemoveTag(Tag tag)
		{
			TagSet.Remove(tag);
		}

		#endregion
	}
}
