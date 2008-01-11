using System;
using System.Collections.Generic;
using System.Data;

namespace Telesophy.Alexandria.Persistence
{
	public interface ICommand
	{
		CommandType Type { get; }
	}
	
	public interface ICommand<Model> : ICommand
	{
		IMap<Model> Map { get; }
		IList<ICommand> SubCommands { get; }
	}
}
