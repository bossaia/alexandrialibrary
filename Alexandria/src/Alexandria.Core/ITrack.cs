using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abraxas;

namespace Alexandria.Core
{
	public interface ITrack
		: IEntity, ITagged
	{
		IArtist Artist { get; }
		IAlbum Album { get; }
		string Title { get; }
		int Number { get; }
		TimeSpan Duration { get; }
	}
}
