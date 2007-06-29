using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	public interface IPersistenceMechanism
	{
		string Name { get; }
		bool IsOpen { get; }
		void Open();
		void Close();
	}
}
