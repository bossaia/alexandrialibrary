using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IChain :
		IResource
	{
		IEnumerable<T> GetValues<T>()
			where T : struct;

		IEnumerable<T> GetReferences<T>()
			where T : class;

		IEnumerable<T> GetResources<T>()
			where T : IResource;

		void SetValues<T>(IEnumerable<T> values)
			where T : struct;

		void SetReferences<T>(IEnumerable<T> references)
			where T : class;

		void SetResources<T>(IEnumerable<T> resources)
			where T : IResource;
		
		uint GetCount();
	}
}
