using System;
using System.Collections.Generic;
using System.Text;
using Alexandria.Media;

namespace Alexandria.Catalog
{
	public interface ICatalogFile
	{
		Uri Path { get; }
		IMediaFormat Format { get; }
	}
}
