using System;
using System.Collections.Generic;
using System.Data;

namespace Telesophy.Alexandria.Persistence
{
	public interface IResult
	{
		bool IsSuccessful { get; }
		int RecordsAffected { get; }
		Exception Error { get; }
		IList<IResult> SubResults { get; }
	}
	
	public interface IResult<Model> : IResult
	{
		IMap<Model> Data { get; }
	}
}
