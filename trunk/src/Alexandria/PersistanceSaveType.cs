using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public enum PersistanceSaveType
	{
		None = 0,
		PropertyGet,
		PropertyGetToString,
		Collection,
		Ignore
	}
}
