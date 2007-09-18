using System;
using System.Collections.Generic;
using System.Globalization;

using Alexandria.Console.Commands;

namespace Alexandria.Console
{
	public static class CommandFactory
	{		
		private static Dictionary<string, Command> commands = new Dictionary<string, Command>(StringComparer.InvariantCultureIgnoreCase);
		
		private static void AddToDictionary(Command command)
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
						AddToDictionary(new AddCommand());
						AddToDictionary(new ContextCommand());
						AddToDictionary(new StatusCommand());
						AddToDictionary(new CloseCommand());
						AddToDictionary(new PlayCommand());
						AddToDictionary(new PauseCommand());
						AddToDictionary(new StopCommand());
						AddToDictionary(new SeekCommand());
						AddToDictionary(new VolumeCommand());
					}
									
					return commands;
				}
			}
		}
	}
}
