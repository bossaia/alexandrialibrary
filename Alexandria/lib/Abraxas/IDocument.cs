using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abraxas
{
	public interface IDocument
		: IResource
	{
		IEntity Entity { get; set; }
		IMedia Media { get; set; }
	}
}
