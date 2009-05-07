using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IWriter<T>
		where T : IResource
	{
		IWriteCriteria<T> CreateWriteCriteria();
		IResponse Write(IRequest request);
	}
}
