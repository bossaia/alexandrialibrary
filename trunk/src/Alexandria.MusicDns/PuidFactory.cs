using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MusicDns
{
	internal static class PuidFactory
	{
		#region Private Constants
		private const string ID_TYPE = "PUID";		
		#endregion
		
		#region Private Static Fields
		private static readonly Version version = new Version(1, 0, 0, 0);
		#endregion
	
		#region Public Static Methods
		internal static IMetadataIdentifier CreatePuid(string value)
		{
			return new MetadataIdentifier(Guid.NewGuid(), Guid.NewGuid(), value, ID_TYPE, version);
		}
		#endregion
	}
}
