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
			this.version = version;
		}		
		#endregion

		#region Private Fields
		private Guid value;
		private MusicDnsIdType type;
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
			if (other != null && other is Puid)
			{
				if (this.Value == other.Value)
					return 0;
				else
					return -1;
			}
			
			return int.MinValue;
		}
		#endregion
	}
}
