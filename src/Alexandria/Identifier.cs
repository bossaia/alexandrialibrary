using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public abstract class Identifier : IIdentifier
	{
		#region Constructors
		public Identifier(string value, string type, Version version)
		{
			this.value = value;
			this.type = type;
			this.version = version;
		}
		#endregion
	
		#region Private Fields
		private string value;
		private string type;
		private Version version;
		#endregion
	
		#region IIdentifier Members
		public string  Value
		{
			get { return value; }
		}

		public string  Type
		{
			get { return type; }
		}
		
		public IVersion Version
		{
			get { return version; }
		}

		public virtual IdentificationResult CompareTo(IIdentifier other)
		{
			if (other != null)
			{
				if (other is Identifier)
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
			return this.value;
		}
		#endregion
	}
}
