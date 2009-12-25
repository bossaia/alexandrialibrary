using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public interface ITag
		: IEntity
	{
		string Type { get; }
		string Name { get; }
	}
}
