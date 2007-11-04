using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Creation
{
	public interface IArtist
	{
		string Name { get; set; }
		DateTime BeginDate { get; set; }
		DateTime EndDate { get; set; }
	}
}
