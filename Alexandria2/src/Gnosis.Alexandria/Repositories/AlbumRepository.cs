using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Factories;
using Gnosis.Alexandria.Mapping;

namespace Gnosis.Alexandria.Repositories
{
	public class AlbumRepository
		: DatabaseRepository<IAlbum>, IAlbumRepository
	{
		public AlbumRepository(IContext context, IFactory<IAlbum> factory, IClassMap<IAlbum> map)
			: base(context, factory, map)
		{
		}

		#region IAlbumRepository Members

		public IList<IAlbum> GetByArtist(IArtist artist)
		{
			string commandText = string.Format("SELECT * FROM {0} WHERE Artist = {1}", Map.Table, artist.Id);

			return List(commandText);
		}

		#endregion
	}
}
