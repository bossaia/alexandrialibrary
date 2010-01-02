using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abraxas;

namespace Alexandria.Core
{
	public interface IAlbum
		: IEntity, ITagged
	{
		string Title { get; }
		IArtist Artist { get; }
		DateTime Released { get; }

		IEntityMap<ITrack> Tracks { get; }
	}
}
