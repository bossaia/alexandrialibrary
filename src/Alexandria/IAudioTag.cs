using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	[System.CLSCompliant(false)]
	public interface IAudioTag
	{
		string Title {get;set;}
		IList<string> Artists {get;set;}
		//string[] Artists_old {get;set;}
		IList<string> Performers {get;set;}
		//string[] Performers_old {get;set;}
		IList<string> Composers {get;set;}
		//string[] Composers_old {get;set;}
		string Album {get;set;}
		string Comment {get;set;}
		IList<string> Genres {get;set;}
		//string[] Genres_old {get;set;}
		uint Year {get;set;}
		uint Track {get;set;}
		uint TrackCount {get;set;}
		uint Disc {get;set;}
		uint DiscCount {get;set;}

		//IPicture[] Pictures { get { return new Picture[] { }; } set { } }

		string FirstArtist {get;}
		string FirstPerformer {get;}
		string FirstComposer {get;}
		string FirstGenre {get;}
		string JoinedArtists {get;}
		string JoinedPerformers {get;}
		string JoinedComposers {get;}
		string JoinedGenres {get;}
	}
}
