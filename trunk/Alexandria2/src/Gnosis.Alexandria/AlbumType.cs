using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct AlbumType
		: IEquatable<AlbumType>
	{
		public AlbumType(string name, string code)
		{
			_name = name;
			_code = code;
		}

		private string _name;
		private string _code;

		public string Name
		{
			get { return _name; }
		}

		public string Code
		{
			get { return _code; }
		}

		public override int GetHashCode()
		{
			return _code.GetHashCode();
		}

		#region IEquatable<AlbumType> Members

		public bool Equals(AlbumType other)
		{
			return GetHashCode() == other.GetHashCode();
		}

		#endregion
	}
}
