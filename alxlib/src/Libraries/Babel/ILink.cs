using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ILink : IResource
	{
		IResource GetValue();
		void SetValue(IResource value);
	}

	public interface ILink<T> : ILink
		where T : IResource
	{
		new T GetValue();
		void SetValue(T value);
	}
}
