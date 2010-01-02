using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abraxas;

namespace Alexandria.Core
{
	public interface ITag
		: IEntity
	{
		string Type { get; }
	}
}
