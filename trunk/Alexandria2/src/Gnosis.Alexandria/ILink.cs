using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface ILink
		: IEntity, IEquatable<ILink>
	{
		IEntity Source { get; }
		IEntity Target { get; }
		LinkType Type { get; }
	}
}
