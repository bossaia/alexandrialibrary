using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ITag
		: IEntity, IEquatable<ITag>
	{
		IEntity Entity { get; }
		object Value { get; }
		TagType Type { get; }
		void ChangeEntity(IEntity entity);
		void ChangeValue(object value);
		void ChangeType(TagType type);
	}
}
