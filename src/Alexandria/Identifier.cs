using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public struct Identifier : IIdentifier
	{
		#region Constructors
		public Identifier(Uri @namespace, Uri value)
		{
			this.@namespace = @namespace;
			this.value = value;
		}
		#endregion
		
		#region Private Fields
		private Uri @namespace;
		private Uri value;
		#endregion

		#region Public Methods
		public override bool Equals(object obj)
		{
			if (obj != null && obj is Identifier)
			{
				Identifier otherIdentifier = (Identifier)obj;
				return (this.Namespace == otherIdentifier.Namespace && this.Value == otherIdentifier.Value);
			}
			else return false;
		}

		public override int GetHashCode()
		{
			return (Namespace.GetHashCode() + Value.GetHashCode());
		}

		public override string ToString()
		{
			return (Value != null) ? Value.ToString() : string.Empty;
		}
		#endregion
	
		#region IIdentifier Members
		public Uri Namespace
		{
			get { return @namespace; }
		}

		public Uri Value
		{
			get { return value; }
		}
		#endregion

		#region IEquatable<IIdentifier> Members
		public bool Equals(IIdentifier other)
		{
			return (Namespace == other.Namespace && Value == other.Value);
		}
		#endregion

		#region Static Members
		private static Identifier undefined = new Identifier(null, null);
		
		public static Identifier Undefined
		{
			get { return undefined; }
		}
		
		public static bool operator ==(Identifier identifier1, Identifier identifier2)
		{
			return identifier1.Equals(identifier2);
		}
		
		public static bool operator !=(Identifier identifier1, Identifier identifier2)
		{
			return !identifier1.Equals(identifier2);
		}
		#endregion
	}
}
