using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class GuidIdentifier : IIdentifier
	{
		#region Constructors
		public GuidIdentifier()
		{
			this.guid = Guid.NewGuid();
		}
		
		public GuidIdentifier(Guid guid)
		{
			this.guid = guid;
		}
		#endregion
	
		#region Private Fields
		private Guid guid = Guid.NewGuid();
		private const string type = "guid";
		private readonly Version version = new Version(1,0, 0, 0);
		#endregion
	
		#region IIdentifier Members
		public string Value
		{
			get { return guid.ToString(); }
		}

		public string Type
		{
			get { return type; }
		}

		public IVersion Version
		{
			get { return version; }
		}

		public IdentificationResult CompareTo(IIdentifier other)
		{
			if (other != null)
			{
				if (other is GuidIdentifier)
				{
					if (this.Value == other.Value)
						return IdentificationResult.Match;
					else return IdentificationResult.IdMismatch;
				}
				else return IdentificationResult.TypeMismatch;
			}
			else return IdentificationResult.None;
		}
		#endregion
	}
}
