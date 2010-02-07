using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Link
		: IEquatable<Link>
	{
		public Link(IEntity source, IEntity target, LinkType type)
		{
			_source = source;
			_target = target;
			_type = type;
		}

		private IEntity _source;
		private IEntity _target;
		private LinkType _type;

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

		public override int GetHashCode()
		{
			return string.Format("{0}|{1}|{2}", _source.Id, _target.Id, _type.Name).GetHashCode();
		}

		#region IEquatable<Link> Members

		public bool Equals(Link other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
