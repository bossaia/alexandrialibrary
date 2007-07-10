using System;
using System.Collections.Generic;
using Alexandria.Metadata;

namespace Alexandria.MusicBrainz
{
	public static class MusicBrainzIdFactory
	{
		#region Private Constants
		private const string ID_TYPE = "MusicBrainzId";
		#endregion

		#region Private Static Fields
		private static readonly Version version = new Version(1, 0, 0, 0);
		#endregion
		
		#region Public Static Methods
		public static MetadataIdentifier CreateMusicBrainzId(IMetadata parent, Guid musicBrainzId)
		{
			return new MetadataIdentifier(Guid.NewGuid(), parent, musicBrainzId.ToString(), ID_TYPE, version);
		}
		#endregion
	}
}
