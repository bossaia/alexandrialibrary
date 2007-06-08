using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public enum PersistanceLoadType
	{
		None = 0,
		ConstructorByName,
		ConstructorByOrdinal,
		PropertySetByName,
		Collection,
		CollectionLazyLoad,
		Ignore
	}
}
