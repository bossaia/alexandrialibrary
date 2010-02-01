using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria
{
	public interface IPerson
		: INamedEntity
	{
		DateTime BornOn { get; }
		Country BornIn { get; }
		DateTime DiedOn { get; }
		ISet<IMedia> Media();

		void Born(DateTime date, Country country);
		void Died(DateTime date);
		void AddMedia(IMedia media);
		void RemoveMedia(IMedia media);
	}
}
