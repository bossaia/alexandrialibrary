using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[System.CLSCompliant(false)]
	public interface IAudioTag
	{
		string Title {get; set;}
		string Album { get; set;}
		string Comment { get; set;}
		IList<string> Artists {get; set;}
		IList<string> Performers {get; set;}
		IList<string> Composers {get; set;}
		IList<string> Genres { get; set;}
		uint Year {get; set;}
		uint Track {get; set;}
		uint TrackCount {get; set;}
		uint Disc {get; set;}
		uint DiscCount {get; set;}
		IList<IImage> Pictures { get; set; }
		//string FirstArtist {get;}
		//string FirstPerformer {get;}
		//string FirstComposer {get;}
		//string FirstGenre {get;}
		//string JoinedArtists {get;}
		//string JoinedPerformers {get;}
		//string JoinedComposers {get;}
		//string JoinedGenres {get;}
	}
}
