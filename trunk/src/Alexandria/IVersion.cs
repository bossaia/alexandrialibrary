using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IVersion : IComparable<IVersion>
	{
		string Name { get; }
		int MajorNumber { get; }
		int MinorNumber { get; }
		int BuildNumber { get; }
		int RevisionNumber { get; }
	}
}
