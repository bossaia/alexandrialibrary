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
		#endregion

		#region IComparable<IIdentifier> Members
		public int CompareTo(IIdentifier other)
		{
			if (other != null && other is MusicBrainzId)
			{
				if (this.Type == other.Type && this.Value == other.Value)
					return 0;
				else
					return -1;
			}
			
			return int.MinValue;
		}
		#endregion
	}
}
