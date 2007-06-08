using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.MusicDns
{
	public class Puid : BaseMetadataIdentifier
	{
		#region Public Enums
		public enum MusicDnsIdType
		{
			MusicDnsId
		}
		#endregion
	
		#region Constructors
		public Puid(string value) : this(Guid.NewGuid(), Guid.NewGuid(), value)
		{
		}
		
		public Puid(Guid id, Guid parentId, string value) : this(id, parentId, value, new Version(1, 0, 0, 0))
		{
		}
		
		public Puid(Guid id, Guid parentId, string value, Version version) : base(id, parentId, value, TYPE, version)
		{
		}
		#endregion

		#region Private Constants
		private const string TYPE = "Puid";
		#endregion

		#region IIdentifier Members
		public override IdentificationResult CompareTo(IIdentifier other)
		{
			if (other != null)
			{
				if (other is Puid)
				{
					if (this.Version.CompareTo(other.Version) == 0)
					{
						if ((string.Compare(this.Type, other.Type, true) == 0) && (string.Compare(this.Value, other.Value, true) == 0) && (this.Version.CompareTo(other.Version) == 0))
							return IdentificationResult.Match;
						else
							return IdentificationResult.IdMismatch;
					}
					else return IdentificationResult.VersionMismatch;
				}
				else return IdentificationResult.TypeMismatch;
			}
			else return IdentificationResult.None;
		}
		#endregion		
	}
}
