using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public class MediaRepository
		: IMediaRepository
	{
		#region IMediaRepository Members

		public bool Exists(Uri id)
		{
			throw new NotImplementedException();
		}

		public IList<IMedia> GetAll()
		{
			throw new NotImplementedException();
		}

		public IList<IMedia> GetByParent(Uri parent)
		{
			throw new NotImplementedException();
		}

		public IList<IMedia> GetByParentId(long id)
		{
			throw new NotImplementedException();
		}

		public IList<IMedia> GetByCriteria(Predicate<IMedia> criteria)
		{
			throw new NotImplementedException();
		}

		public void Save(IMedia media)
		{
			throw new NotImplementedException();
		}

		public void Delete(Uri id)
		{
			throw new NotImplementedException();
		}

		public void Move(Uri source, Uri destination)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
