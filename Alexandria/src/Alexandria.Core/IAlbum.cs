using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public interface IAlbum
		: IEntity, ITagged
	{
		string Title { get; }
		IArtist Artist { get; }
		DateTime Released { get; }

		IEntityList<ITrack> Tracks { get; }
	}
}
