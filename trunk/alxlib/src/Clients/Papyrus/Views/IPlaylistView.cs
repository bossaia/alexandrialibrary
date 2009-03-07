using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Papyrus.Data;

namespace Papyrus.Views
{
	public interface IPlaylistView : IView
	{
		DataItem<string> Title { get; }
		DataItem<string> Creator { get; }
		DataItem<DateTime> Created { get; }
		DataItem<Uri> Info { get; }
		DataItem<Uri> Location { get; }
		DataItem<Uri> Identifier { get; }
		DataItem<Uri> Image { get; }
		DataItem<Uri> License { get; }
		DataItem<string> Comment { get; }
		DataList<AttributionData> Attribution { get; }
		DataList<LinkData> Links { get; }
		DataList<MetaData> Metadata { get; }
		DataList<ExtensionData> Extensions { get; }
		DataList<TrackData> Tracks { get; }
	}
}
