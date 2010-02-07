using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface INamed
		: IEntity
	{
		Name Name { get; }

		void Rename(Name name);
	}
}
