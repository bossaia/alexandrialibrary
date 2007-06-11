using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.Mp3Tunes
{
	internal static class TrackIdFactory
	{
		#region Private Constants
		private const string ID_TYPE = "MP3tunes TrackID";
		#endregion
		
		#region Private Static Fields
		private static readonly Version version = new Version(1, 0, 0, 0);
		#endregion
		
		#region Public Static Methods
		public static IMetadataIdentifier CreateTrackId(string value)
		{
			return new MetadataIdentifier(Guid.NewGuid(), Guid.NewGuid(), value, ID_TYPE, version);
		}
		#endregion
	}
}
