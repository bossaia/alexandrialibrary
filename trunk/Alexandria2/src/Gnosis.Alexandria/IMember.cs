using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IMember
		: INamed, IEquatable<IMember>
	{
		IArtist Group { get; }
		IArtist Individual { get; }
		DateTime Joined { get; }
		DateTime Left { get; }
		IEnumerable<Role> Roles();

		void ChangeGroup(IArtist group);
		void ChangeIndividual(IArtist individual);
		void ChangeJoined(DateTime joined);
		void ChangeLeft(DateTime left);

		void AddRole(Role role);
		void RemoveRole(Role role);
	}
}
