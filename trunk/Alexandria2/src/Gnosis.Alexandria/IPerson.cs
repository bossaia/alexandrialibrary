using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria
{
	public interface IPerson
		: INamedEntity
	{
		DateTime? DateBorn { get; }
		Country CountryBorn { get; }
		DateTime? DateDied { get; }
		ISet<IMedia> Media();

		void ChangeDateBorn(DateTime? dateBorn);
		void ChangeCountryBorn(Country countryBorn);
		void ChangeDateDied(DateTime? dateDied);
		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
