using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IReader<T>
		where T : IResource
	{
		IReadCriteria<T> CreateReadCriteria();
		IResult Read(ICommand command);
	}
}
