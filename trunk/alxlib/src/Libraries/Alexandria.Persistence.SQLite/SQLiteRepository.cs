using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Alexandria.Persistence;
using Alexandria.Resources.Media;

namespace Alexandria.Persistence.SQLite
{
	public class SQLiteRepository : IRepository
	{
		public SQLiteRepository()
		{
		}

		#region IRepository Members

		public T GetOne<T>(IFilter filter)
		{
			if (typeof(T) == typeof(Album))
			{
			}
			else if (typeof(T) == typeof(Artist))
			{
			}
			else if (typeof(T) == typeof(File))
			{
			}
			else if (typeof(T) == typeof(Playlist))
			{
			}
			else if (typeof(T) == typeof(Track))
			{
			}
			else
			{
			}

			return default(T);
		}

		public IList<T> GetMany<T>(IFilter filter)
		{
			throw new NotImplementedException();
		}

		public void SaveOne<T>(T value)
		{
			throw new NotImplementedException();
		}

		public void SaveMany<T>(IEnumerable<T> values)
		{
			throw new NotImplementedException();
		}

		public void DeleteOne<T>(T value)
		{
			throw new NotImplementedException();
		}

		public void DeleteMany<T>(IEnumerable<T> values)
		{
			throw new NotImplementedException();
		}

		public IFilter BuildFilter(FilterType type, string name, Operator op, object value)
		{
			throw new NotImplementedException();
		}

		public IFilter BuildFilter(FilterType type, string name, Operator op, object value, IFilter child)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
