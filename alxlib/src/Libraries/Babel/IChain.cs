using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IChain : IResource
	{
		IEnumerable<IResource> GetValues();
		void SetValues(IEnumerable<IResource> values);
		long GetCount();
	}

	public interface IChain<T> : IChain
		where T : IResource
	{
		new IEnumerable<T> GetValues();
		void SetValues(IEnumerable<T> values);
	}
}
