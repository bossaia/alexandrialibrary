using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Babel
{
	public interface ILink :
		IResource
	{
		T GetValue<T>()
			where T : struct;

		T GetReference<T>()
			where T : class;

		T GetResource<T>()
			where T : IResource;

		void SetValue<T>(T value)
			where T : struct;

		void SetReference<T>(T reference)
			where T : class;

		void SetResource<T>(T resource)
			where T : IResource;
	}
}
