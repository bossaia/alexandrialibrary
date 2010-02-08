using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface IMediaRepository
	{
		bool Exists(Uri id);
		IList<IMedia> GetAll();
		IList<IMedia> GetByParent(Uri parent);
		IList<IMedia> GetByParentId(long id);
		IList<IMedia> GetByCriteria(Predicate<IMedia> criteria);

		void Save(IMedia media);
		void Delete(Uri id);
		void Move(Uri source, Uri destination);
	}
}
