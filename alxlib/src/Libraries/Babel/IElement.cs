using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IElement
		: IResource
	{
		int GetCount();

		T GetFirstValue<T>();

		IEnumerable<T> GetValues<T>();

		void SetFirstValue<T>(T value);

		void SetValues<T>(IEnumerable<T> values);
	}
}
