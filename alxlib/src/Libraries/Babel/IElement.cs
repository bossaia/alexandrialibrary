using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface IElement
	{
		T GetValue<T>();

		void SetValue<T>(T value);
	}
}
