using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	public class Mp3TunesTrackIdentifier : BaseMetadataIdentifier
	{
		#region Constructors
		public Mp3TunesTrackIdentifier(string value) : this(Guid.NewGuid(), Guid.NewGuid(), value)
		{
		}
		
		public Mp3TunesTrackIdentifier(Guid id, Guid parentId, string value) : base(id, parentId, value, TYPE, new Version(1,0,0,0))
		{
		}
		#endregion
		
		#region Private Constants
		private const string TYPE = "MP3tunes TrackID";
		#endregion
	}
}
