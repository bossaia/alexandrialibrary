using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public interface IArtist
		: IEntity, ITagged
	{
		string Name { get; }

		IEntityList<IAlbum> Albums { get; }
	}
}
