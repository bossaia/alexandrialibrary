using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alexandria.Resources
{
	public interface ILinkBuilder
	{
		string Name { get; }
		Uri Type { get; }
		int Cardinality { get; }
		bool IsRequired { get; }
		bool IsTopDown { get; }
		void LoadData(IEntity entity);
		void LoadData(IEnumerable<IEntity> entity);
	}

	public interface ILinkBuilder<T>
		where T : IEntity
	{
		T GetOne();
		IEnumerable<T> GetMany();
	}
}
