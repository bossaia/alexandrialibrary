using System;
using System.Collections.Generic;
using System.Globalization;

using Alexandria.Console.Commands;

namespace Alexandria.Console
{
	public class CommandFactory
	{
		public CommandFactory()
		{
			AddCommand(new StatusCommand());
			AddCommand(new CloseCommand());
			
			AddCommand(new PlayCommand());
			AddCommand(new PauseCommand());
			AddCommand(new StopCommand());
			AddCommand(new SeekCommand());
			AddCommand(new VolumeCommand());
		}
		
		private IDictionary<string, Command> commands = new Dictionary<string, Command>(StringComparer.InvariantCultureIgnoreCase);
		
		private void AddCommand(Command command)
		{
			if (command != null && !commands.ContainsKey(command.Name))
				commands.Add(command.Name, command);
		}
		
		public IDictionary<string, Command> Commands
		{
			get { return commands; }
		}
	}
}
