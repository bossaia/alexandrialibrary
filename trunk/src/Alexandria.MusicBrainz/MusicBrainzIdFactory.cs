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
		public static IMetadataIdentifier CreateMusicBrainzId(IMetadata parent, Guid musicBrainzId)
		{
			IMetadataIdentifier identifier = new MetadataIdentifier(Guid.NewGuid(), musicBrainzId.ToString(), ID_TYPE, version);
			identifier.MetadataParent = parent;
			return identifier;
		}
		#endregion
	}
}
