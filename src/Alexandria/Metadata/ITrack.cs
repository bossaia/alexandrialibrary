using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface ITrack : IMetadataItem
	{
		Work Album { get; set; }
		Creator Artist { get; set; }
		Duration Duration { get; set; }
		Title Name { get; set; }
		ReleaseDate Date { get; set; }
		ItemNumber TrackNumber { get; set; }
	}
}
