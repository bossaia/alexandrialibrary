using System;
using System.Collections.Generic;
using System.Text;

using Telesophy.Alexandria.Media;
using Telesophy.Alexandria.Resources;

namespace Telesophy.Alexandria.Catalog
{
	public interface ICatalogEntry
	{
		ICatalog Catalog { get; set; }
		IResource Resource { get; set; }
		IList<IWork> Works { get; }
	}
}