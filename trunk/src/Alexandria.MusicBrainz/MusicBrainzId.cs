using System;
using System.Collections.Generic;
using System.Text;
using Alexandria;

namespace Alexandria.MusicBrainz
{
	public class MusicBrainzId : IIdentifier
	{
		#region Public Enum
		public enum MusicBrainzIdType
		{
			MusicBrainzId = 0,
			MusicBrainzReleaseId,
			MusicBrainzArtistId,
			MusicBrainzTrackId,
			MusicBrainzTrmId,
		}
		#endregion
	
		#region Constructors
		public MusicBrainzId(Guid value, MusicBrainzIdType type)
		{
			this.value = value;
			this.type = type;
		}
		#endregion
	
		#region Private Fields
		private Guid value;
		private MusicBrainzIdType type = MusicBrainzIdType.MusicBrainzId;
		private readonly Version version = new Version(1, 0, 0, 0);
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
				if (other is MusicBrainzId)
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
		
		#region Public Methods
		public override string ToString()
		{			
			return this.value.ToString();
		}
		#endregion
	}
}
