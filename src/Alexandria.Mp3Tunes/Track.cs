using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;
using Alexandria.Data;

namespace Alexandria.Mp3Tunes
{
	internal class Track : BaseAudioTrack
	{
		#region Constructors
		public Track(string id, string location, string name, string album, string artist, TimeSpan duration, DateTime releaseDate, int trackNumber, string localName) : base(id, location, name, album, artist, duration, releaseDate, trackNumber, localName)
		{
		}		
		#endregion
	}
}
