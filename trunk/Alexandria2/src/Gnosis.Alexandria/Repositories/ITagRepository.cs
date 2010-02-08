using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface ITagRepository
	{
		IEnumerable<Tag> GetByEntity(IEntity entity);
	}
}
