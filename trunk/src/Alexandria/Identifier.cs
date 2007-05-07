using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public class Identifier : IIdentifier
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
	
		#region Private Static Fields
		private static Identifier none;
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
		#endregion
		
		#region Public Static Properties
		public static IIdentifier None
		{
			get
			{
				if (none == null)
					none = new Identifier(string.Empty, string.Empty, new Version(0, 0, 0, 0));
			
				return none;
			}
		}
		#endregion

		#region IComparable<IIdentifier> Members
		public int CompareTo(IIdentifier other)
		{
			if (other != null && other is Identifier)
			{
				if ((string.Compare(this.Type, other.Type, true) == 0) && (string.Compare(this.Value, other.Value, true) == 0) && (this.Version.CompareTo(other.Version) == 0))
					return 0;
				else
					return -1;
			}
			
			return int.MinValue;
		}
		#endregion
	}
}
