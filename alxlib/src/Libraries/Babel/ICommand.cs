using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ICommand
	{
		IResourceCollection Source { get; }
		IResourceCollection Target { get; }

		void AddReadCriteria<T>(IReadCriteria<T> criteria)
			where T : IResource;

		void AddWriteCriteria<T>(IWriteCriteria<T> criteria)
			where T : IResource;

		void AddTranslateCriteria<X, Y>(ITranslateCriteria<X, Y> criteria)
			where X : IResource
			where Y : IResource;
	}
}
