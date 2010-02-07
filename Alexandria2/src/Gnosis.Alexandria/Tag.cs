using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Tag
		: IEquatable<Tag>
	{
		public Tag(IEntity entity, object value, TagType type)
		{
			_entity = entity;
			_value = value;
			_type = type;
		}

		private IEntity _entity;
		private object _value;
		private TagType _type;

		public IEntity Entity
		{
			get { return _entity; }
		}

		public object Value
		{
			get { return _value; }
		}

		public TagType Type
		{
			get { return _type; }
		}

		public override int GetHashCode()
		{
			return string.Format("{0}|{1}|{2}", _entity.Id, _value.GetHashCode(), _type.Name).GetHashCode();
		}

		#region IEquatable<Tag> Members

		public bool Equals(Tag other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
