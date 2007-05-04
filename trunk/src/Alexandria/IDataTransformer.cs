using System;
using System.Collections.Generic;
using System.Text;

namespace Alexandria
{
	public interface IDataTransformer
	{
		bool CanTransform<T>() where T: IMetadata;
		T GetMetadata<T>(IDataMatrix matrix) where T : IMetadata;
		IDataMatrix GetMatrix(IMetadata metadata);
	}
}
