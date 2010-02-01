using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IMember
		: INamedEntity, IEquatable<IMember>
	{
		DateTime Joined { get; }
		DateTime Left { get; }
		IGroup Group { get; }
		IPerson Person { get; }
		ISet<Role> Roles();

		void Join(DateTime date);
		void Leave(DateTime date);
		void AddRole(Role role);
		void RemoveRole(Role role);
	}
}
