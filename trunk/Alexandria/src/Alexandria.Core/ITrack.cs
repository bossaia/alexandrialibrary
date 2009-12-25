using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Core
{
	public interface ITrack
		: IEntity, ITagged
	{
		IAlbum Album { get; }
		int Number { get; }

		string Name { get; }
		IArtist Artist { get; }
		TimeSpan Duration { get; }
	}
}
