using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public struct Role
		: IEquatable<Role>
	{
		public Role(string name)
		{
			_name = name;
		}

		private readonly string _name;

		public string Name
		{
			get { return _name; }
		}

		#region Operators

		public static bool operator ==(Role role1, Role role2)
		{
			return role1.Equals(role2);
		}

		public static bool operator !=(Role role1, Role role2)
		{
			return !role1.Equals(role2);
		}

		#endregion

		#region Override

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != typeof(Role))
				return false;

			return Equals((Role)obj);
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

		#region IEquatable<Role> Members

		public bool Equals(Role other)
		{
			return _name == other._name;
		}

		#endregion
	}
}
