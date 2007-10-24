using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Metadata
{
	public interface IMediaItem
	{
		string Source { get; set; }
		string Type { get; set; }
		int Number { get; set; }
		string Title { get; set; }
		string Artist { get; set; }
		string Album { get; set; }
		TimeSpan Duration { get; set; }
		DateTime Date { get; set; }
		string Format { get; set; }
		Uri Path { get; set; }
	}
}
