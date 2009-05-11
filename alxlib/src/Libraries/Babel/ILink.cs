using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ILink :
		IElement
	{
		new T GetValue<T>()
			where T : IResource;

		new void SetValue<T>(T value)
			where T : IResource;
	}
}
