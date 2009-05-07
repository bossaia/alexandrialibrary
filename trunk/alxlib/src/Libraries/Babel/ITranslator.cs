using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ITranslator<X, Y>
		where X : IResource
		where Y : IResource
	{
		ITranslateCriteria<X, Y> CreateTranslateCriteria();
		IResponse Translate(IRequest request);
	}
}
