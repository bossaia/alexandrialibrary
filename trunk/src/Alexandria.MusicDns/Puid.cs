using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.MusicDns
{
	public class Puid : IIdentifier
	{
		#region Public Enums
		public enum MusicDnsIdType
		{
			MusicDnsId
		}
		#endregion
	
		#region Constructors
		public Puid(Guid value)
		{
			this.value = value;
			this.type = MusicDnsIdType.MusicDnsId;
			this.version = new Version(1, 0, 0, 0);
		}
		
		public Puid(Guid value, Version version)
		{
			this.value = value;
			this.type = MusicDnsIdType.MusicDnsId;
			this.version = version;
		}
		#endregion

		#region Private Fields
		private Guid value;
		private MusicDnsIdType type;
		private Version version;		
		#endregion

		#region IIdentifier Members
		public string Value
		{
			get { return value.ToString(); }
		}

		public string Type
		{
			get { return type.ToString(); }
		}
		
		public IVersion Version
		{
			get { return version; }
		}

		public IdentificationResult CompareTo(IIdentifier other)
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
