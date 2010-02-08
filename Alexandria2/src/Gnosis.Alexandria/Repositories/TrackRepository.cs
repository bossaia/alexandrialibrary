using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public class TrackRepository
		: ITrackRepository
	{
		#region ITrackRepository Members

		public IList<ITrack> GetAll()
		{
			throw new NotImplementedException();
		}

		public IList<ITrack> GetByAlbum(IAlbum album)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
