using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public class Link
		: EntityBase, ILink
	{
		public Link(IContext context)
			: base(context)
		{
		}

		public Link(IContext context, long id)
			: base(context, id)
		{
		}

		private IEntity _source;
		private IEntity _target;
		private LinkType _type;

		public override int GetHashCode()
		{
			return string.Format("{0}|{1}|{2}", _source.Id, _target.Id, _type.Name).GetHashCode();
		}

		#region ILink Members

		public IEntity Source
		{
			get { return _source; }
		}

		public IEntity Target
		{
			get { return _target; }
		}

		public LinkType Type
		{
			get { return _type; }
		}

		public void ChangeSource(IEntity source)
		{
			_source = source;
		}

		public void ChangeTarget(IEntity target)
		{
			_target = target;
		}

		public void ChangeType(LinkType type)
		{
			_type = type;
		}

		#endregion

		#region IEquatable<ILink> Members

		public bool Equals(ILink other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
