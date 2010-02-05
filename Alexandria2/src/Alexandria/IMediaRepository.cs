using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IMediaRepository
	{
		bool Exists(Uri id);
		IEnumerable<IMedia> Get();
		IEnumerable<IMedia> Get(Uri parent);
		IEnumerable<IMedia> Get(Predicate<IMedia> criteria);

		void Save(IMedia media);
		void Delete(Uri id);
		void Move(Uri source, Uri destination);
	}
}
