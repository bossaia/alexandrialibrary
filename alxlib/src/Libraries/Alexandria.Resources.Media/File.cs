using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources.Media
{
	public class File : SubjectAggregate
	{
		public File(Uri id, IEntityType type)
			: base(id, type)
		{
		}
	}
}
