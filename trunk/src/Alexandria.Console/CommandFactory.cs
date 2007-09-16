using System;
using System.Collections.Generic;
using System.Globalization;

using Alexandria.Console.Commands;

namespace Alexandria.Console
{
	public static class CommandFactory
	{		
		private static Dictionary<string, Command> commands = new Dictionary<string, Command>(StringComparer.InvariantCultureIgnoreCase);
		
		private static void AddCommand(Command command)
		{
			if (!commands.ContainsKey(command.Name))
				commands.Add(command.Name, command);
		}
		
		public static bool IsCommand(string name)
		{
			return Commands.ContainsKey(name);
		}
		
		public static IDictionary<string, Command> Commands
		{
			get
			{
				lock(commands)
				{
					if (commands.Count == 0)
					{
						AddCommand(new ContextCommand());
						AddCommand(new StatusCommand());
						AddCommand(new CloseCommand());
						AddCommand(new PlayCommand());
						AddCommand(new PauseCommand());
						AddCommand(new StopCommand());
						AddCommand(new SeekCommand());
						AddCommand(new VolumeCommand());
					}
									
					return commands;
				}
			}
		}
	}
}
