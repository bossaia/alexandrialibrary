using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria.MusicDns
{
	/// <summary>
	/// This is a wrapper around genpuid.exe (make sure to pass the key from NativeMethods)
	/// </summary>
	public class PuidFactory
	{
		#region Private Constants
		private const string musicDnsKey = "5fe4267b3a269f9463cd2a7f14ac6406";
		#endregion
	
		#region Public Static Methods
		public Puid GetPuid(ILocation location)
		{
			return null;
		}
		#endregion
	
		//genpuid dns-key [options] [file1] [file2] ...
		
		//MusicBrainz lookup
		//http://musicbrainz.org/show/puid/?puid=2e6d085b-bf25-10d7-4bce-66f21de0e798
	}
}
