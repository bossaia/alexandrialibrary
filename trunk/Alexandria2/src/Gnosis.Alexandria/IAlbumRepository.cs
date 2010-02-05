using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IAlbumRepository
	{
		IEnumerable<IAlbum> GetAll();
		IEnumerable<IAlbum> GetByArtist(IArtist artist);
		IEnumerable<IAlbum> GetByCriteria(Predicate<IAlbum> criteria);

		void Save(IAlbum album);
		void Delete(long id);
	}
}
