using System;
using Telesophy.Alexandria.Core;

namespace Telesophy.Alexandria.Model
{
	public interface IArtist : IEntity
	{
		string Type { get; set; }
		string Name { get; set; }
		DateTime BeginDate { get; set; }
		DateTime EndDate { get; set; }
	}
}
