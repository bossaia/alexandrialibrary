using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Persistence
{
	[Flags]
	public enum FieldCascade
	{
		None = 0,
		Save = 1,
		Delete = 2,
		All = Save | Delete
	}
}
