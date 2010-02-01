using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IAlbum
		: INamedEntity, IEquatable<IAlbum>
	{
		IGroup Artist { get; }
		ISet<IRelease> Releases();

		void ChangeArtist(IGroup group);
		void AddRelease(IRelease release);
		void RemoveRelease(IRelease release);
	}
}
