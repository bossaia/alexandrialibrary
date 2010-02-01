using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public struct Name
		: IComparable<Name>, IEquatable<Name>
	{
		public Name(string original, string normalized, string hash)
		{
			_original = original;
			_normalized = normalized;
			_hash = hash;
		}

		private readonly string _original;
		private readonly string _normalized;
		private readonly string _hash;

		public string Original
		{
			get { return _original; }
		}

		public string Normalized
		{
			get { return _normalized; }
		}

		public string Hash
		{
			get { return _hash; }
		}

		#region Static Members

		public static readonly Name Empty = new Name(string.Empty, string.Empty, string.Empty);

		#endregion

		#region Operators

		public static bool operator ==(Name name1, Name name2)
		{
			return name1.Equals(name2);
		}

		public static bool operator !=(Name name1, Name name2)
		{
			return !name1.Equals(name2);
		}

		public static bool operator >(Name name1, Name name2)
		{
			return name1.CompareTo(name2) > 0;
		}

		public static bool operator >=(Name name1, Name name2)
		{
			return name1.CompareTo(name2) >= 0;
		}

		public static bool operator <(Name name1, Name name2)
		{
			return name1.CompareTo(name2) < 0;
		}

		public static bool operator <=(Name name1, Name name2)
		{
			return name1.CompareTo(name2) <= 0;
		}

#endregion

		#region Overrides

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Name))
				return false;

			return Equals((Name)obj);
		}

		public override int GetHashCode()
		{
			return _normalized.GetHashCode();
		}

		public override string ToString()
		{
			return _normalized;
		}

		#endregion

		#region IComparable<Name> Members

		public int CompareTo(Name other)
		{
			return _normalized.CompareTo(other._normalized);
		}

		#endregion

		#region IEquatable<Name> Members

		public bool Equals(Name other)
		{
			return _normalized == other._normalized;
		}

		#endregion
	}
}
