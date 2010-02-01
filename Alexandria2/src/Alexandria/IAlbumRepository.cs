using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IAlbumRepository
	{
		IEnumerable<IAlbum> GetAll();
		IEnumerable<IAlbum> GetByArtist(IGroup artist);
		IEnumerable<IAlbum> GetByCriteria(Predicate<IAlbum> criteria);

		void Save(IAlbum album);
		void Delete(long id);
	}
}
