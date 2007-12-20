using System;
using System.Collections.Generic;
using System.Text;

namespace Telesophy.Alexandria.Model
{
	public interface IArtist
	{
		Guid Id { get; set; }
		string Type { get; set; }
		string Name { get; set; }
		DateTime BeginDate { get; set; }
		DateTime EndDate { get; set; }
	}
}
