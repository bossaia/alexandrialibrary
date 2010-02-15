using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Repositories;

namespace Gnosis.Alexandria
{
	public interface IContext
	{
		IAlbumRepository Albums { get; }
		IAlbumTypeRepository AlbumTypes { get; }
		IArtistRepository Artists { get; }
		ICountryRepository Countries { get; }
		ILinkRepository Links { get; }
		IMediaRepository Media { get; }
		ITagRepository Tags { get; }
		ITrackRepository Tracks { get; }

		void Initialize();
		void Persist(IChangeSet changeSet);
	}
}
