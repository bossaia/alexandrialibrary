using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IArtist
		: INamed, IEquatable<IArtist>
	{
		DateTime Date { get; }
		Country Country { get; }
		IEnumerable<IMember> Members();
		IEnumerable<IAlbum> Albums();
		IEnumerable<IVideo> Videos();

		void ChangeDate(DateTime date);
		void ChangeCountry(Country country);
		
		void AddMember(IMember member);
		void RemoveMember(IMember member);

		void AddAlbum(IAlbum album);
		void RemoveAlbum(IAlbum album);

		void AddVideo(IVideo video);
		void RemoveVideo(IVideo video);
	}
}
