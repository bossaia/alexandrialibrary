using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abraxas;

namespace Alexandria.Core
{
	public interface IArtist
		: IEntity, ITagged
	{
		string Name { get; }
		IEntityMap<IAlbum> Albums { get; }
	}
}
