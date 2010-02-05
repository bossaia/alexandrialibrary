using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Country
		: IEquatable<Country>
	{
		public Country(string name, string code)
		{
			_name = name;
			_code = code;
		}

		private readonly string _name;
		private readonly string _code;

		public string Name
		{
			get { return _name; }
		}

		public string Code
		{
			get { return _code; }
		}


		#region Operators

		public static bool operator ==(Country country1, Country country2)
		{
			return country1.Equals(country2);
		}

		public static bool operator !=(Country country1, Country country2)
		{
			return !country1.Equals(country2);
		}

		#endregion

		#region Override

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Country))
				return false;

			return Equals((Country)obj);
		}

		public override int GetHashCode()
		{
			return _name.GetHashCode();
		}

		public override string ToString()
		{
			return _name;
		}

		#endregion

		#region IEquatable<Country> Members

		public bool Equals(Country other)
		{
			return _name == other._name;
		}

		#endregion
	}
}
