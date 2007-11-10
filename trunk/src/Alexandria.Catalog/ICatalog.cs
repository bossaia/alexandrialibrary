using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Catalog
{
	public interface ICatalog
	{
		string Name { get; set; }
		string Description { get; set; }
		IUser User { get; set; }
	}
}
