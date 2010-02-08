using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface ITrackRepository
	{
		IList<ITrack> GetAll();
		IList<ITrack> GetByAlbum(IAlbum album);
	}
}
