using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	public class Mp3TunesTrackIdentifier : Identifier
	{
		#region Constructors
		public Mp3TunesTrackIdentifier(string value) : base(value, "MP3tunes TrackID", new Version(1,0,0,0))
		{
		}
		#endregion
	}
}
