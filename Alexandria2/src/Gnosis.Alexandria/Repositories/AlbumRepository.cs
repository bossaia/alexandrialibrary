using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public class AlbumRepository
		: DatabaseRepository, IAlbumRepository
	{
		#region IAlbumRepository Members

		public IList<IAlbum> GetAll()
		{
			List<IAlbum> albums = new List<IAlbum>();

			using (var connection = GetConnection())
			{
				connection.Open();
			}

			return albums;
		}

		public IList<IAlbum> GetByArtist(IArtist artist)
		{
			throw new NotImplementedException();
		}

		public IList<IAlbum> GetByCriteria(Predicate<IAlbum> criteria)
		{
			throw new NotImplementedException();
		}

		public void Save(IAlbum album)
		{
			throw new NotImplementedException();
		}

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
