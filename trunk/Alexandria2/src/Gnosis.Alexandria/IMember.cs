using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IMember
		: INamedEntity, IEquatable<IMember>
	{
		DateTime? DateJoined { get; }
		DateTime? DateLeft { get; }
		IArtist Group { get; }
		IPerson Person { get; }
		ISet<Role> Roles();

		void ChangeDateJoined(DateTime? dateJoined);
		void ChangeDateLeft(DateTime? dateLeft);
		void AddRole(Role role);
		void RemoveRole(Role role);
	}
}
