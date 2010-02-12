using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface IArtistRepository
	{
		IArtist Get(long id);
		IEnumerable<IArtist> GetAll();
		IEnumerable<IArtist> GetByName(string name);
	}
}
