using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface IAlbum : IMetadataItem
	{
		Creator Artist { get; set; }
		ReleaseDate Date { get; set; }
		IList<ITrack> Tracks { get; }
	}
}
