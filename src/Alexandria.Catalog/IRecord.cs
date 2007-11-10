using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Catalog
{
	public interface IRecord<T> : IDisposable
	{
		Guid Id { get; set; }
		T Data { get; }
		void Initialize();
		void Save();
		void Delete();
	}
}
