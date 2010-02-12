using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
	public interface IRepository<T>
		where T : IEntity
	{
		T Create();
		T Get(long id);
		IList<T> GetAll();

		void Initialize();
		void Save(T entity);
		void Delete(long id);
	}
}
