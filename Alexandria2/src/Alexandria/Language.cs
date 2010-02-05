using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Language
		: IEquatable<Language>
	{
		public Language(string name, string code)
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
			get { return _name; }
		}


		#region Operators

		public static bool operator ==(Language language1, Language language2)
		{
			return language1.Equals(language2);
		}

		public static bool operator !=(Language language1, Language language2)
		{
			return !language1.Equals(language2);
		}

		#endregion

		#region Override

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Language))
				return false;

			return Equals((Language)obj);
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

		#region IEquatable<Language> Members

		public bool Equals(Language other)
		{
			return _name == other._name;
		}

		#endregion
	}
}
