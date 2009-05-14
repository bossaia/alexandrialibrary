using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ILink :
		IElement
	{
		new T GetFirstValue<T>()
			where T : IResource;

		new IEnumerable<T> GetValues<T>()
			where T : IResource;

		new void SetFirstValue<T>(T value)
			where T : IResource;

		new void SetValues<T>(IEnumerable<T> values)
			where T : IResource;
	}
}
