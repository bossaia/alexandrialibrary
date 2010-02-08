using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface IAlbumRepository
	{
		IList<IAlbum> GetAll();
		IList<IAlbum> GetByArtist(IArtist artist);
		IList<IAlbum> GetByCriteria(Predicate<IAlbum> criteria);

		void Save(IAlbum album);
		void Delete(long id);
	}
}
