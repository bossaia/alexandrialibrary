using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Name
		: IComparable<Name>, IEquatable<Name>
	{
		public Name(string name)
		{
			_originalName = name;
			_displayName = name;
			_searchName = name;
			_hash = name;
		}

		private readonly string _originalName;
		private readonly string _displayName;
		private readonly string _searchName;
		private readonly string _hash;

		public string OriginalName
		{
			get { return _originalName; }
		}

		public string DisplayName
		{
			get { return _displayName; }
		}

		public string SearchName
		{
			get { return _searchName; }
		}

		public string Hash
		{
			get { return _hash; }
		}

		#region Static Members

		public static readonly Name Empty = new Name(string.Empty);

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
			return _searchName.GetHashCode();
		}

		public override string ToString()
		{
			return _displayName;
		}

		#endregion

		#region IComparable<Name> Members

		public int CompareTo(Name other)
		{
			return _searchName.CompareTo(other._searchName);
		}

		#endregion

		#region IEquatable<Name> Members

		public bool Equals(Name other)
		{
			return _searchName == other._searchName;
		}

		#endregion
	}
}
