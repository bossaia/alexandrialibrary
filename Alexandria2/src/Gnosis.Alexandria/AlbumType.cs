using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct AlbumType
		: IEquatable<AlbumType>
	{
		public AlbumType(string name)
		{
			_name = name;
		}

		private string _name;

		public string Name
		{
			get { return _name; }
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		#region IEquatable<AlbumType> Members

		public bool Equals(AlbumType other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
