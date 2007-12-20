#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

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
						AddToDictionary(new CloseCommand());
						AddToDictionary(new ContextCommand());
						AddToDictionary(new ListCommand());
						AddToDictionary(new LoadCommand());
						AddToDictionary(new PauseCommand());
						AddToDictionary(new PlayCommand());
						AddToDictionary(new SaveCommand());
						AddToDictionary(new SeekCommand());
						AddToDictionary(new StatusCommand());
						AddToDictionary(new StopCommand());
						AddToDictionary(new VolumeCommand());
					}
									
					return commands;
				}
			}
		}
	}
}
