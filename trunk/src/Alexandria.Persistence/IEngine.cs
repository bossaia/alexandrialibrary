using System;
using System.Collections.Generic;
using System.Data;

namespace Telesophy.Alexandria.Persistence
{
	public interface IEngine
	{
		ICommand<Model> GetInitializeCommand<Model>(CommandType type, IMap<Model> map, Guid id);
		IResult<Model> Execute<Model>(ICommand<Model> command);
	}
}
