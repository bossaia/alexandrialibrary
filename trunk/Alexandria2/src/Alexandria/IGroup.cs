using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IGroup
		: INamedEntity
	{
		DateTime FormedOn { get; }
		Country FormedIn { get; }
		DateTime DisbandedOn { get; }
		ISet<IMember> Members();
		ISet<IMedia> Media();

		void Formed(DateTime date, Country country);
		void Disbanded(DateTime date);
		void AddMember(IMember member);
		void RemoveMember(IMember member);
		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
