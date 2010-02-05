using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abraxas
{
	public interface IDocumentGroup
	{
		IEntity Entity { get; set; }
		IList<IMedia> Media { get; }

		//TODO: Add media-handling behavior methods
	}
}
