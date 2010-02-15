using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public class Tag
		: EntityBase, ITag
	{
		public Tag(IContext context)
			: base(context)
		{
		}

		public Tag(IContext context, long id)
			: base(context, id)
		{
		}

		private IEntity _entity;
		private object _value;
		private TagType _type;

		#region ITag Members

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

		public void ChangeEntity(IEntity entity)
		{
			_entity = entity;
		}

		public void ChangeValue(object value)
		{
			_value = value;
		}

		public void ChangeType(TagType type)
		{
			_type = type;
		}

		#endregion

		public override int GetHashCode()
		{
			return string.Format("{0}|{1}|{2}", _entity.Id, _value.GetHashCode(), _type.Name).GetHashCode();
		}

		#region IEquatable<ITag> Members

		public bool Equals(ITag other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
