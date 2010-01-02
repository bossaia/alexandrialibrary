using System;

namespace Alexandria.LastFM.LastRipper
{

	///<summary>
	///Representation of an recorded or currently playing number.
	///</summary>
	public interface IMetaMusic : IMetaTrack
	{
		System.String Album
		{
			get;
		}
		System.String Trackduration
		{
			get;
		}
	}

	///<summary>
	///Sortest possible amount of data to regonize a track.
	///</summary>
	public interface IMetaTrack
	{
		System.String Track
		{
			get;
		}
		System.String Artist
		{
			get;
		}
	}
}
